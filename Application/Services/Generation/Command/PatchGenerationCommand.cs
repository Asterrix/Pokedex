using Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Services.Generation.Command;

public record PatchGenerationCommand(string GenerationName, JsonPatchDocument<Models.Generation> PatchDocument) : IRequest<bool>;

public class PatchGenerationCommandHandler : IRequestHandler<PatchGenerationCommand, bool>
{
    private readonly IGenerationRepository _generationRepository;
    private readonly IValidator<Models.Generation> _validator;

    public PatchGenerationCommandHandler(
        IGenerationRepository generationRepository,
        IValidator<Models.Generation> validator)
    {
        _generationRepository = generationRepository;
        _validator = validator;
    }

    public async Task<bool> Handle(PatchGenerationCommand request, CancellationToken cancellationToken)
    {
        #region Validation

        var generation = await _generationRepository.GetGenerationAsync(request.GenerationName.Trim(), cancellationToken);
        if (generation is null)
        {
            throw new NotFoundException(
                $"Generation with the name of \"{request.GenerationName.Trim()}\" could not be found."
            );
        }

        request.PatchDocument.ApplyTo(generation);
        generation.Name = generation.Name.Trim();

        var newGeneration = await _generationRepository.GetGenerationAsync(generation.Name, cancellationToken);
        if (newGeneration is not null)
        {
            throw new InvalidOperationException(
                $"New value you are trying to assign to \"{request.GenerationName.Trim()}\" conflicts with existing generation."
            );
        }

        await _validator.ValidateAndThrowAsync(generation, cancellationToken);

        #endregion

        return await _generationRepository.PatchGenerationAsync(generation, cancellationToken);
    }
}