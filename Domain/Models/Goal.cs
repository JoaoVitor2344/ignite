namespace ignite.Domain.Models;

public class Goal : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
