using Application.ViewModels.Gender;
using Application.ViewModels.Statistic;

namespace Application.ViewModels.Pokemon;

public record PokemonGetViewModel(
    Guid Guid,
    string Name,
    string Portrait,
    float Height,
    float Weight,
    GenderViewModel Gender,
    string Description,
    string Generation,
    string Specie,
    string Category,
    StatisticGetViewModel Statistics);