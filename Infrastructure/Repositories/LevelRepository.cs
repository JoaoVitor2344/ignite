using ignite.Domain.Entities;
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

        public async Task<IEnumerable<Level>> GetAllAsync()
        {
            return await _context.Levels.ToListAsync();
        }

        public async Task<Level?> GetByIdAsync(Guid id)
        {
            return await _context.Levels
                .Where(l => l.DeletedAt == null && l.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Level level)
        {
            _context.Levels.Add(level);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Level level)
        {
            level.UpdatedAt = DateTime.UtcNow;
            _context.Levels.Update(level);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var level = await GetByIdAsync(id);
            if (level == null)
            {
                throw new KeyNotFoundException("Level not found");
            }
            level.DeletedAt = DateTime.UtcNow;
            _context.Levels.Update(level);
            await _context.SaveChangesAsync();
        }
    }
}