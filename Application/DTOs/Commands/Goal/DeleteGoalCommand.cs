using System.ComponentModel.DataAnnotations;

namespace ignite.Application.DTOs.Commands.Goal;

public class DeleteGoalCommand
{
    public Guid Id { get; set; }
} 