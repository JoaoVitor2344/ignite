using ignite.Application.Commands.UserCommands;
using ignite.DTOs;

namespace ignite.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetUsersAsync();
    Task<UserResponseDto?> GetUserByIdAsync(Guid id);
    Task<Guid> CreateUserAsync(CreateUserCommand command);
    Task UpdateUserAsync(UpdateUserCommand command);
    Task DeleteUserAsync(Guid id);
}