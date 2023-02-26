using Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Services.Pokemon.Command;

public record PatchPokemonCommand(string Name, JsonPatchDocument<Models.Pokemon> PatchDocument) : IRequest<bool>;

public class PatchPokemonCommandHandler : IRequestHandler<PatchPokemonCommand, bool>
{
    private readonly IValidator<Models.Pokemon> _validator;
    private readonly IPokemonRepository _pokemonRepository;

    public PatchPokemonCommandHandler(IValidator<Models.Pokemon> validator, IPokemonRepository pokemonRepository)
    {
        _validator = validator;
        _pokemonRepository = pokemonRepository;
    }

    public async Task<bool> Handle(PatchPokemonCommand request, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonRepository.GetPokemonAsync(request.Name.Trim(), cancellationToken);
        if (pokemon is null)
        {
            throw new NotFoundException($"Pokemon \"{request.Name.Trim()}\" does not exist.");
        }

        request.PatchDocument.ApplyTo(pokemon);

        /* Trim string object values */
        pokemon.Name = pokemon.Name.Trim();
        pokemon.Description = pokemon.Description.Trim();
        pokemon.Portrait = pokemon.Portrait.Trim();

        var checkNewValue = await _pokemonRepository.GetPokemonAsync(pokemon.Name, cancellationToken);
        if (checkNewValue is not null)
        {
            throw new InvalidOperationException(
                $"There already exists a pokemon with the name of {pokemon.Name.Trim()}"
            );
        }

        await _validator.ValidateAndThrowAsync(pokemon, cancellationToken);

        return await _pokemonRepository.PatchPokemonAsync(pokemon, cancellationToken);
    }
}