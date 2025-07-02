using ignite.Domain.Entities;
using ignite.DTOs;

namespace ignite.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> CreateUserAsync(CreateUserDto dto);
    }
}
