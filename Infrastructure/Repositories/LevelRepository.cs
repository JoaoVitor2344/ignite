using ignite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ignite.Infrastructure.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly AppDbContext _context;

        public LevelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.Level>> GetAllAsync()
        {
            return await _context.Levels.ToListAsync();
        }

        public async Task AddAsync(Domain.Entities.Level level)
        {
            _context.Levels.Add(level);
            await _context.SaveChangesAsync();
        }
    }
}