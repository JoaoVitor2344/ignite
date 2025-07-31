namespace ignite.Domain.Models;

public class Level : BaseEntity
{
    public required string Name { get; set; }
    public required double Min { get; set; }
    public required double Max { get; set; }
}