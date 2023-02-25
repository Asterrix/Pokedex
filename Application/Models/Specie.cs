namespace Application.Models;

public class Specie
{
    public Guid Id { get; } = new Guid();
    public string Name { get; set; } = null!;
}