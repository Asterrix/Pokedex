namespace Application.Models;

public class Generation
{
    public Guid Id { get; } = new Guid();
    public string Name { get; set; } = null!;
}