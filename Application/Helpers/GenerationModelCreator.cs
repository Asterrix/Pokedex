using Application.Models;
using Application.Services.Generation.Command;

namespace Application.Helpers;

internal static class GenerationModelCreator
{
    internal static Generation CreateGenerationModel(CreateGenerationCommand command)
    {
        var entity = new Generation
        {
            Name = command.Name.Trim()
        };
        return entity;
    }
}