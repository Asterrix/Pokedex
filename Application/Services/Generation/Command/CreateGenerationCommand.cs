using Application.Contracts;
using Application.Helpers;
using FluentValidation;
using MediatR;

namespace Application.Services.Generation.Command;

public record CreateGenerationCommand(string Name) : IRequest<Models.Generation>;

public class CreateGenerationCommandHandler : IRequestHandler<CreateGenerationCommand, Models.Generation>
{
    private readonly IGenerationRepository _generationRepository;
    private readonly IValidator<Models.Generation> _validator;

    public CreateGenerationCommandHandler(
        IValidator<Models.Generation> validator,
        IGenerationRepository generationRepository)
    {
        _validator = validator;
        _generationRepository = generationRepository;
    }

    public async Task<Models.Generation> Handle(CreateGenerationCommand request, CancellationToken cancellationToken)
    {
        var generation = await _generationRepository.GetGenerationAsync(request.Name.Trim(), cancellationToken);
        if (generation is not null)
        {
            throw new InvalidOperationException(
                $"Generation with the name of \"{request.Name.Trim()}\" already exists.");
        }

        var entity = ModelCreator.CreateGenerationModel(request);

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        return await _generationRepository.CreateGenerationAsync(entity, cancellationToken);
    }
}