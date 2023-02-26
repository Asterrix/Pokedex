using Application.Contracts;
using Application.Models;
using Application.ViewModels;
using Application.ViewModels.Gender;
using Application.ViewModels.Statistic;
using FluentValidation;
using MediatR;

namespace Application.Services.Pokemon.Command;

public record CreatePokemonCommand(
    string Name,
    string Portrait,
    float Height,
    float Weight,
    GenderViewModel Gender,
    string Description,
    string Generation,
    string Specie,
    string Category,
    StatisticPostViewModel Statistics) : IRequest<Models.Pokemon>;

public class CreatePokemonCommandHandler : IRequestHandler<CreatePokemonCommand, Models.Pokemon>
{
    private readonly IValidator<Models.Pokemon> _validator;
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IGenerationRepository _generationRepository;
    private readonly ISpecieRepository _specieRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreatePokemonCommandHandler(
        IValidator<Models.Pokemon> validator,
        IPokemonRepository pokemonRepository,
        IGenerationRepository generationRepository,
        ISpecieRepository specieRepository,
        ICategoryRepository categoryRepository)
    {
        _validator = validator;
        _pokemonRepository = pokemonRepository;
        _generationRepository = generationRepository;
        _specieRepository = specieRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Models.Pokemon> Handle(CreatePokemonCommand request, CancellationToken cancellationToken)
    {
        #region Validation

        var pokemon = await _pokemonRepository.GetPokemonAsync(request.Name.Trim(), cancellationToken);
        if (pokemon is not null)
        {
            throw new InvalidOperationException($"Pokemon with the name of \"{request.Name.Trim()}\" already exists.");
        }

        var generation = await _generationRepository.GetGenerationAsync(request.Generation.Trim(), cancellationToken);
        if (generation is null)
        {
            throw new NotFoundException($"Generation with the name of \"{request.Generation.Trim()}\" does not exist.");
        }

        var specie = await _specieRepository.GetSpecieAsync(request.Specie.Trim(), cancellationToken);
        if (specie is null)
        {
            throw new NotFoundException($"Specie with the name of \"{request.Specie.Trim()}\" does not exist.");
        }

        var category = await _categoryRepository.GetCategoryAsync(request.Category.Trim(), cancellationToken);
        if (category is null)
        {
            throw new NotFoundException($"Category with the name of \"{request.Category.Trim()}\" does not exist.");
        }

        var entity = new Models.Pokemon
        {
            Name = request.Name.Trim(),
            Portrait = request.Portrait.Trim(),
            Height = request.Height,
            Weight = request.Weight,
            Gender = new Gender
            {
                Male = request.Gender.Male,
                Female = request.Gender.Female
            },
            Description = request.Description,
            Generation = generation,
            Specie = specie,
            Category = category,
            Statistic = new Statistic
            {
                Hp = request.Statistics.Hp,
                Attack = request.Statistics.Attack,
                Defense = request.Statistics.Defense,
                SpecialAttack = request.Statistics.SpecialAttack,
                SpecialDefense = request.Statistics.SpecialDefense,
                Speed = request.Statistics.Speed
            }
        };

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        #endregion


        return await _pokemonRepository.CreatePokemonAsync(entity, cancellationToken);
    }
}