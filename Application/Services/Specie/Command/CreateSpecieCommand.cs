using Application.Contracts;
using FluentValidation;
using MediatR;

namespace Application.Services.Specie.Command;

public record CreateSpecieCommand(string Name) : IRequest<Models.Specie>;

public class CreateSpecieCommandHandler : IRequestHandler<CreateSpecieCommand, Models.Specie>
{
    private readonly ISpecieRepository _specieRepository;
    private readonly IValidator<Models.Specie> _validator;

    public CreateSpecieCommandHandler(
        ISpecieRepository specieRepository,
        IValidator<Models.Specie> validator)
    {
        _specieRepository = specieRepository;
        _validator = validator;
    }

    public async Task<Models.Specie> Handle(CreateSpecieCommand request, CancellationToken cancellationToken)
    {
        var specie = await _specieRepository.GetSpecieAsync(request.Name.Trim(), cancellationToken);
        if (specie is not null)
        {
            throw new InvalidOperationException($"Specie with the name of \"{request.Name.Trim()}\" already exists.");
        }

        var entity = new Models.Specie
        {
            Name = request.Name.Trim()
        };

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);

        return await _specieRepository.CreateSpecieAsync(entity, cancellationToken);
    }
}