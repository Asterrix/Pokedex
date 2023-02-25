using Application.Contracts;
using FluentValidation;
using MediatR;

namespace Application.Services.Generation.Command;

public record PatchGenerationCommand(string Name, string NewValue) : IRequest<bool>;

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

        var sameValue = String.Equals(
            request.Name.Trim(),
            request.NewValue.Trim(),
            StringComparison.OrdinalIgnoreCase);

        if (sameValue)
        {
            throw new InvalidOperationException("Generation name cannot be the same as the old one.");
        }

        var generation = await _generationRepository.GetGenerationAsync(request.Name.Trim());
        if (generation is null)
        {
            throw new NotFoundException(
                $"Generation with the name of \"{request.Name.Trim()}\" could not be found."
            );
        }

        var newGeneration = await _generationRepository.GetGenerationAsync(request.NewValue.Trim());
        if (newGeneration is not null)
        {
            throw new InvalidOperationException(
                $"New value you are trying to assign to \"{request.Name.Trim()}\" conflicts with existing generation."
            );
        }


        var entity = new Models.Generation
        {
            Name = request.NewValue.Trim()
        };

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        #endregion

        return await _generationRepository.PatchGenerationAsync(generation, request.Name.Trim());
    }
}