using Application.ViewModels.Category;

namespace Application.ViewModels.Pokemon;

public record PokemonListGetViewModel
(
    Guid Id,
    string Name,
    string Portrait,
    List<CategoryRelationViewModel> Categories
);

