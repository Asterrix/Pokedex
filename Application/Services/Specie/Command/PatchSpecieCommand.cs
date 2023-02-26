using Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Services.Specie.Command;

public record PatchSpecieCommand(string Name, JsonPatchDocument<Models.Specie> PatchDocument) : IRequest<bool>;

public class PatchSpecieCommandHandler : IRequestHandler<PatchSpecieCommand, bool>
{
    private readonly ISpecieRepository _specieRepository;
    private readonly IValidator<Models.Specie> _validator;

    public PatchSpecieCommandHandler(ISpecieRepository specieRepository, IValidator<Models.Specie> validator)
    {
        _specieRepository = specieRepository;
        _validator = validator;
    }

    public async Task<bool> Handle(PatchSpecieCommand request, CancellationToken cancellationToken)
    {
        #region Validation
        
        var specie = await _specieRepository.GetSpecieAsync(request.Name.Trim(), cancellationToken);
        if (specie is null)
        {
            throw new NotFoundException($"Specie with the name of \"{request.Name.Trim()}\" could not be found.");
        }

        request.PatchDocument.ApplyTo(specie);
        specie.Name = specie.Name.Trim();
        
        var newSpecie = await _specieRepository.GetSpecieAsync(specie.Name.Trim(), cancellationToken);
        if (newSpecie is not null)
        {
            throw new InvalidOperationException($"Specie with the name of {specie.Name.Trim()} already exists.");
        }

        #endregion

        await _validator.ValidateAndThrowAsync(specie, cancellationToken);

        return await _specieRepository.PatchSpecieAsync(specie, cancellationToken);
    }
}