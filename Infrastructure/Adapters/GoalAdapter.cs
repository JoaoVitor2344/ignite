using ignite.Application.DTOs.Response;
using ignite.Domain.Models;

namespace ignite.Infrastructure.Adapters;

public static class GoalAdapter
{
    public static GoalResponseDto? ToDto(Goal? goal)
    {
        if (goal == null)
        {
            return null;
        }

        return new GoalResponseDto
        {
            Id = goal.Id, // Ensure goal.Id is of type Guid
            Name = goal.Name,
            Description = goal.Description
        };
    }
} 