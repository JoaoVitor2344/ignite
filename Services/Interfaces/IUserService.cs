using ignite.Domain.Entities;
using ignite.DTOs;

namespace ignite.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(UserDto dto);
        Task UpdateUserAsync(Guid id, UserDto user);
        Task DeleteUserAsync(Guid id);
    }
}
