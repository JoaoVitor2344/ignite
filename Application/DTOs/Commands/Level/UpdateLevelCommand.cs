namespace ignite.Application.DTOs.Commands.Level;

public class UpdateLevelCommand
{
    public required string Name { get; set; }
    public required double Min { get; set; }
    public required double Max { get; set; }
}
