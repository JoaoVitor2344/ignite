using ignite.Application.DTOs.Response;
using ignite.Infrastructure.Adapters;
using ignite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Services.Queries;

public class LevelQueryService
{
    private readonly AppDbContext _context;

    public LevelQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LevelResponseDto>> GetAllLevelsAsync()
    {
        var levels = await _context.Levels.AsNoTracking().Where(x => x.DeletedAt == null).ToListAsync();
        return levels.Select(LevelAdapter.ToDto)!.ToList()!;
    }

    public async Task<LevelResponseDto?> GetLevelByIdAsync(Guid id)
    {
        var level = await _context.Levels.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
        return LevelAdapter.ToDto(level);
    }
}
