using ignite.Application.DTOs.Commands.Level;
using ignite.Domain.Models;
using ignite.Infrastructure.Data;
using ignite.Infrastructure.Adapters;
using ignite.Application.DTOs.Response;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Services.Handlers;

public class LevelHandlerService
{
    private readonly AppDbContext _context;

    public LevelHandlerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LevelResponseDto> HandleAsync(CreateLevelCommand command)
    {
        var level = new Level
        {
            Name = command.Name,
            Min = command.Min,
            Max = command.Max,
            CreatedAt = DateTime.UtcNow
        };
        _context.Levels.Add(level);
        await _context.SaveChangesAsync();
        return LevelAdapter.ToDto(level)!;
    }

    public async Task<LevelResponseDto?> HandleAsync(Guid id, UpdateLevelCommand command)
    {
        var level = await _context.Levels.FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
        if (level == null) return null;
        level.Name = command.Name;
        level.Min = command.Min;
        level.Max = command.Max;
        level.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return LevelAdapter.ToDto(level);
    }

    public async Task<bool> HandleAsync(DeleteLevelCommand command)
    {
        var level = await _context.Levels.FirstOrDefaultAsync(x => x.Id == command.Id && x.DeletedAt == null);
        if (level == null) return false;
        level.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}
