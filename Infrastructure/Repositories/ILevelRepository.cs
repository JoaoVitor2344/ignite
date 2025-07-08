using ignite.Domain.Entities;

namespace ignite.Infrastructure.Repositories
{
    public interface ILevelRepository
    {
        Task<IEnumerable<Level>> GetAllAsync();
        Task<Level?> GetByIdAsync(Guid id);
        Task AddAsync(Level level);
        Task UpdateAsync(Level level);
        Task DeleteAsync(Guid id);
    }
}