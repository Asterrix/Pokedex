using Application.Models;
using Application.Services.Category.Command;

namespace Application.Helpers;

internal static class CategoryModelCreator
{
    internal static Category CreateCategoryModel(CreateCategoryCommand command)
    {
        var entity = new Category()
        {
            Name = command.Name.Trim()
        };
        return entity;
    }
}