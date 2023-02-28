namespace Application.Models;

public class CategoryRelation
{
    public Guid Id { get; } = new Guid();
    public Guid PokemonId { get; } = new Guid();
    public Category Category { get; set; } = null!;
}