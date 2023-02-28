using Application.Models;
using Application.Services.Specie.Command;

namespace Application.Helpers;

internal static class SpecieModelCreator
{
    internal static Specie CreateSpecieModel(CreateSpecieCommand command)
    {
        var entity = new Specie
        {
            Name = command.Name.Trim()
        };
        return entity;
    }
}