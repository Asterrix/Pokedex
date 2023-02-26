using Application.Contracts;
using FluentValidation;
using MediatR;

namespace Application.Services.Specie.Command;

public record PatchSpecieCommand(string Name, string NewValue) : IRequest<bool>;

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

        var sameValue = String.Equals(request.Name.Trim(), request.NewValue.Trim(), StringComparison.OrdinalIgnoreCase);
        if (sameValue)
        {
            throw new InvalidOperationException("Specie name cannot be the same as the old one.");
        }

        var specie = await _specieRepository.GetSpecieAsync(request.Name.Trim(), cancellationToken);
        if (specie is null)
        {
            throw new NotFoundException($"Specie with the name of \"{request.Name.Trim()}\" could not be found.");
        }

        var newSpecie = await _specieRepository.GetSpecieAsync(request.NewValue.Trim(), cancellationToken);
        if (newSpecie is not null)
        {
            throw new InvalidOperationException($"Nev value you are trying to assign to \"{request.Name.Trim()}\" conflicts with existing specie.");
        }

        #endregion

        var entity = new Models.Specie()
        {
            Name = request.NewValue.Trim()
        };

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        return await _specieRepository.PatchSpecieAsync(specie, cancellationToken);
    }
}