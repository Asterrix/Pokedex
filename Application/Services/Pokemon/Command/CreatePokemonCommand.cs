using Application.Contracts;
using Application.Helpers;
using Application.Mapper;
using Application.ViewModels.Gender;
using Application.ViewModels.Pokemon;
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
    List<Models.Category> Category,
    StatisticPostViewModel Statistics) : IRequest<PokemonGetViewModel>;

public class CreatePokemonCommandHandler : IRequestHandler<CreatePokemonCommand, PokemonGetViewModel>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IGenerationRepository _generationRepository;
    private readonly IPokemonRepository _pokemonRepository;
    private readonly ISpecieRepository _specieRepository;
    private readonly IValidator<Models.Pokemon> _validator;

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

    public async Task<PokemonGetViewModel> Handle(CreatePokemonCommand request, CancellationToken cancellationToken)
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


        #region Grab Categories & Validate Their Existance

        var listCategories = new List<Models.Category>();

        foreach (var category in request.Category)
        {
            var categoryExists = await _categoryRepository.GetCategoryAsync(category.Name.Trim(), cancellationToken);
            if (categoryExists is null)
            {
                throw new NotFoundException($"Category with the name of \"{category.Name.Trim()}\" does not exist.");
            }

            listCategories.Add(categoryExists);
        }

        #endregion


        var entity = ModelCreator.CreatePokemonModel(request, generation, specie, listCategories);

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        #endregion

        var result = await _pokemonRepository.CreatePokemonAsync(entity, cancellationToken);

        return PokemonMapper.ToPokemonGetViewModel(ref result);
    }
}