namespace Application.Models;

public class Category
{
    public Guid Id { get; } = new Guid();
    public string Name { get; set; } = null!;
}