using ignite.Application.DTOs.Commands.Category;
using ignite.Application.DTOs.Response;
using ignite.Domain.Models;
using ignite.Infrastructure.Adapters;
using ignite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Services.Handlers
{
    public class CategoryHandlerService
    {
        private readonly AppDbContext _context;

        public CategoryHandlerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponseDto> HandleAsync(CreateCategoryCommand command)
        {
            var category = new Category { Name = command.Name, Description = command.Description };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CategoryAdapter.ToDto(category)!;
        }

        public async Task<CategoryResponseDto?> HandleAsync(Guid id, UpdateCategoryCommand command)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null);

            if (category == null)
                return null;

            if (!string.IsNullOrEmpty(command.Name))
                category.Name = command.Name;

            category.Description = command.Description;
            category.UpdatedAt = DateTime.UtcNow;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return CategoryAdapter.ToDto(category)!;
        }

        public async Task<bool> HandleAsync(DeleteCategoryCommand command)
        {
            var category = await _context
            .Categories.Where(c => c.DeletedAt == null && c.Id == command.Id)
            .FirstOrDefaultAsync();

            if (category == null)
                return false;

            category.DeletedAt = DateTime.UtcNow;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
