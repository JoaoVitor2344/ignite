using ignite.DTOs;
using ignite.Domain.Entities;

namespace ignite.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserDto dto);
    }
}
