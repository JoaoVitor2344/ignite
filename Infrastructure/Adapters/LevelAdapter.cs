using ignite.Domain.Models;
using ignite.Application.DTOs.Response;

namespace ignite.Infrastructure.Adapters;

public static class LevelAdapter
{
    public static LevelResponseDto? ToDto(Level? level)
    {
        if (level == null) return null;
        return new LevelResponseDto
        {
            Id = level.Id,
            Name = level.Name,
            Min = level.Min,
            Max = level.Max,
            CreatedAt = level.CreatedAt.ToString("o"),
            UpdatedAt = level.UpdatedAt?.ToString("o"),
            DeletedAt = level.DeletedAt?.ToString("o")
        };
    }
}
