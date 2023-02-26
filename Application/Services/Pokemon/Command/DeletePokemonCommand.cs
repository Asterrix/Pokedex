using Application.Contracts;
using MediatR;

namespace Application.Services.Pokemon.Command;

public record DeletePokemonCommand(string Name) : IRequest<bool>;

public class DeletePokemonCommandHandler : IRequestHandler<DeletePokemonCommand, bool>
{
    private readonly IPokemonRepository _pokemonRepository;

    public DeletePokemonCommandHandler(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<bool> Handle(DeletePokemonCommand request, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonAsync(request.Name.Trim(), cancellationToken);
        if (pokemon is null)
        {
            throw new NotFoundException($"Pokemon with the name of \"{request.Name.Trim()}\" could not be found");
        }

        return await _pokemonRepository.DeletePokemonAsync(pokemon, cancellationToken);
    }
}