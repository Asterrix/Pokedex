namespace Application.Models;

public class Pokemon
{
    public Guid Id { get; } = new Guid();
    public string Name { get; set; } = null!;
    public string Portrait { get; set; } = null!;
    public float Height { get; set; }
    public float Weight { get; set; }
    public Gender Gender { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Generation Generation { get; set; } = null!;
    public Specie Specie { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public Statistic Statistic { get; set; } = null!;
}