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

        await _validator.ValidateAndThrowAsync(pokemon, cancellationToken);
        
        var result = await _pokemonRepository.PatchPokemonAsync(pokemon, cancellationToken);
        return result;
    }
}