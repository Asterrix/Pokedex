using Application.Contracts;
using MediatR;

namespace Application.Services.Specie.Command;

public record DeleteSpecieCommand(string Name) : IRequest<bool>;

public class DeleteSpecieCommandHandler : IRequestHandler<DeleteSpecieCommand, bool>
{
    private readonly ISpecieRepository _specieRepository;

    public DeleteSpecieCommandHandler(ISpecieRepository specieRepository)
    {
        _specieRepository = specieRepository;
    }

    public async Task<bool> Handle(DeleteSpecieCommand request, CancellationToken cancellationToken)
    {
        var specie = await _specieRepository.GetSpecieAsync(request.Name.Trim());
        if (specie == null)
        {
            throw new NotFoundException($"Specie with the name of \"{request.Name.Trim()}\" could not be found.");
        }

        return await _specieRepository.DeleteSpecieAsync(specie);
    }
}