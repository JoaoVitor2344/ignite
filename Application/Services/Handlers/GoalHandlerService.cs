using ignite.Application.DTOs.Response;
using ignite.Application.DTOs.Commands.Goal;
using ignite.Infrastructure.Adapters;
using ignite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ignite.Domain.Models;

namespace ignite.Application.Services.Handlers;

public class GoalHandlerService
{
    private readonly AppDbContext _context;

    public GoalHandlerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GoalResponseDto> HandleAsync(CreateGoalCommand command)
    {
        var goal = new Goal { Name = command.Name, Description = command.Description };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        return GoalAdapter.ToDto(goal)!;
    }

    public async Task<GoalResponseDto?> HandleAsync(Guid id, UpdateGoalCommand command)
    {
        var goal = await _context.Goals
        .FirstOrDefaultAsync(g => g.Id == id && g.DeletedAt == null);

        if (goal == null)
            return null;

        if (!string.IsNullOrEmpty(command.Name))
            goal.Name = command.Name;

        goal.Description = command.Description;
        goal.UpdatedAt = DateTime.UtcNow;

        _context.Goals.Update(goal);
        await _context.SaveChangesAsync();

        return GoalAdapter.ToDto(goal)!;
    }

    public async Task<bool> HandleAsync(DeleteGoalCommand command)
    {
        var goal = await _context
            .Goals.Where(g => g.DeletedAt == null && g.Id == command.Id)
            .FirstOrDefaultAsync();

        if (goal == null)
            return false;

        goal.DeletedAt = DateTime.UtcNow;
        _context.Goals.Update(goal);
        await _context.SaveChangesAsync();

        return true;
    }
}
