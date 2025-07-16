using ignite.Application.DTOs.Response;
using ignite.Infrastructure.Adapters;
using ignite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Services.Queries;

public class GoalQueryService
{
    private readonly AppDbContext _context;

    public GoalQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GoalResponseDto>> GetAllGoalsAsync()
    {
        var goals = await _context.Goals.Where(g => g.DeletedAt == null).ToListAsync();

        return goals.Select(goal => GoalAdapter.ToDto(goal)!);
    }

    public async Task<GoalResponseDto?> GetGoalByIdAsync(Guid id)
    {
        var goal = await _context
            .Goals.Where(g => g.DeletedAt == null && g.Id == id)
            .FirstOrDefaultAsync();

        return GoalAdapter.ToDto(goal);
    }
}
