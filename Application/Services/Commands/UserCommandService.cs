using julius.Application.DTOs.Commands.User;
using julius.Application.DTOs.Response;
using julius.Application.Services.Handlers;

namespace julius.Application.Services.Commands;

public class UserCommandService
{
    private readonly UserHandlerService _handler;
    
    public UserCommandService(UserHandlerService handler)
    {
        _handler = handler;
    }

    public async Task<UserDTO> CreateUserAsync(CreateUserCommand command)
    {
        return await _handler.HandleAsync(command);
    }

    public async Task<UserDTO?> UpdateUserAsync(UpdateUserCommand command)
    {
        return await _handler.HandleAsync(command);
    }

    public async Task<bool> DeleteUserAsync(DeleteUserCommand command)
    {
        return await _handler.HandleAsync(command);
    }
} 