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
        public DbSet<Category> Categories { get; set; }

        public async Task SeedAsync(AppDbContext context)
        {
            Console.WriteLine("--- [INICIANDO SEEDER] ---");

            try
            {
                await context.Database.MigrateAsync();
                Console.WriteLine("[SEEDER] Migrações aplicadas com sucesso.");

                Console.WriteLine("[SEEDER] Verificando se o usuário 'admin@ignite.com' existe...");
                bool adminExists = await context.Users.AnyAsync(u => u.Email == "admin@ignite.com");

                if (!adminExists)
                {
                    Console.WriteLine("[SEEDER] Usuário admin NÃO encontrado. Criando novo usuário...");

                    var adminUser = new User
                    {
                        Name = "Admin",
                        Email = "admin@ignite.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        CreatedAt = DateTime.UtcNow
                    };

                    await context.Users.AddAsync(adminUser);
                    await context.SaveChangesAsync();

                    Console.WriteLine("[SEEDER] Usuário admin criado e salvo no banco de dados com sucesso!");
                }
                else
                {
                    Console.WriteLine("[SEEDER] Usuário admin JÁ EXISTE no banco de dados. Nenhuma ação necessária.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--- [ERRO NO SEEDER] --- Ocorreu uma exceção: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("--- [FINALIZANDO SEEDER] ---");
            }
        }
    }
}
