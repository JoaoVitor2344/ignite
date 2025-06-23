using ignite.Domain.Entities;

namespace ignite.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
