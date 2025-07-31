namespace ignite.Application.DTOs.Commands.Level;

public class CreateLevelCommand
{
    public required string Name { get; set; }
    public required double Min { get; set; }
    public required double Max { get; set; }
}
