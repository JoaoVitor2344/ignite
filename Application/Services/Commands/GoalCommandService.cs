using ignite.Application.DTOs.Response;
using ignite.Application.DTOs.Commands.Goal;
using ignite.Application.Services.Handlers;

namespace ignite.Application.Services.Commands;

public class GoalCommandService
{
    private readonly GoalHandlerService _handler;

    public GoalCommandService(GoalHandlerService handler)
    {
        _handler = handler;
    }

    public async Task<GoalResponseDto> CreateGoalAsync(CreateGoalCommand command)
    {
        return await _handler.HandleAsync(command);
    }

    public async Task<GoalResponseDto?> UpdateGoalAsync(Guid id, UpdateGoalCommand command)
    {
        return await _handler.HandleAsync(id, command);
    }

    public async Task<bool> DeleteGoalAsync(DeleteGoalCommand command)
    {
        return await _handler.HandleAsync(command);
    }
}
