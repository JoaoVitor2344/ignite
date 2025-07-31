namespace ignite.Application.DTOs.Response;

public class LevelResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Min { get; set; }
    public double Max { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
    public string? UpdatedAt { get; set; }
    public string? DeletedAt { get; set; }
}
