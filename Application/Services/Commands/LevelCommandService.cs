using ignite.Application.DTOs.Commands.Level;
using ignite.Application.DTOs.Response;
using ignite.Application.Services.Handlers;

namespace ignite.Application.Services.Commands;

public class LevelCommandService
{
    private readonly LevelHandlerService _handler;

    public LevelCommandService(LevelHandlerService handler)
    {
        _handler = handler;
    }

    public Task<LevelResponseDto> CreateLevelAsync(CreateLevelCommand command)
        => _handler.HandleAsync(command);

    public Task<LevelResponseDto?> UpdateLevelAsync(Guid id, UpdateLevelCommand command)
        => _handler.HandleAsync(id, command);

    public Task<bool> DeleteLevelAsync(DeleteLevelCommand command)
        => _handler.HandleAsync(command);
}
