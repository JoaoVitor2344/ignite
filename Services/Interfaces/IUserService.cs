using ignite.Domain.Entities;
using ignite.DTOs;

namespace ignite.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(CreateUserDto dto);
        Task UpdateUserAsync(Guid id, UpdateUserDto user);
        Task DeleteUserAsync(Guid id);
    }
}
