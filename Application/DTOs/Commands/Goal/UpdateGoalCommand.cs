using System.ComponentModel.DataAnnotations;

namespace ignite.Application.DTOs.Commands.Goal;

public class UpdateGoalCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
} 