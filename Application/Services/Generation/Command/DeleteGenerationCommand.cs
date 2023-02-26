using Application.Contracts;
using MediatR;

namespace Application.Services.Generation.Command;

public record DeleteGenerationCommand(string Name) : IRequest<bool>;

public class DeleteGenerationCommandHandler : IRequestHandler<DeleteGenerationCommand, bool>
{
    private readonly IGenerationRepository _generationRepository;

    public DeleteGenerationCommandHandler(IGenerationRepository generationRepository)
    {
        _generationRepository = generationRepository;
    }

    public async Task<bool> Handle(DeleteGenerationCommand request, CancellationToken cancellationToken)
    {
        var generation = await _generationRepository.GetGenerationAsync(request.Name.Trim(), cancellationToken);
        if (generation == null)
        {
            throw new NotFoundException(
                $"Generation with the name of \"{request.Name.Trim()}\" could not be found."
            );
        }

        return await _generationRepository.DeleteGenerationAsync(generation, cancellationToken);
    }
}