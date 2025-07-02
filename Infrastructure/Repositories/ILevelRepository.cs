namespace ignite.Infrastructure.Repositories
{
    public interface ILevelRepository
    {
        Task<IEnumerable<Domain.Entities.Level>> GetAllAsync();
        Task AddAsync(Domain.Entities.Level level);
    }
}