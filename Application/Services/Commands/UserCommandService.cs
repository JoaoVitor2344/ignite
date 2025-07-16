using ignite.Application.DTOs.Commands.User;
using ignite.Application.Services.Handlers;
using ignite.Application.DTOs.Response;

namespace ignite.Application.Services.Commands;

public class UserCommandService
{
    private readonly UserHandlerService _handler;

    public UserCommandService(UserHandlerService handler)
    {
        _handler = handler;
    }

    public async Task<UserResponseDto> CreateUserAsync(CreateUserCommand command)
    {
        return await _handler.HandleAsync(command);
    }

    public async Task<UserResponseDto?> UpdateUserAsync(UpdateUserCommand command)
    {
        return await _handler.HandleAsync(command);
    }

    public async Task<bool> DeleteUserAsync(DeleteUserCommand command)
    {
        return await _handler.HandleAsync(command);
    }
}
