using Application.ViewModels.Category;
using Application.ViewModels.Gender;
using Application.ViewModels.Statistic;

namespace Application.ViewModels.Pokemon;

public record PokemonGetViewModel(
    Guid Id,
    string Name,
    string Portrait,
    float Height,
    float Weight,
    GenderViewModel Gender,
    string Description,
    string Generation,
    string Specie,
    List<CategoryRelationViewModel> Categories,
    StatisticGetViewModel Statistics);