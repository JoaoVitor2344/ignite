using ignite.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ignite.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Goal> Goals { get; set; }
    }
}
