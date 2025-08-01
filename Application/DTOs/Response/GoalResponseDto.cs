namespace ignite.Application.DTOs.Response;

public class GoalResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
} 