using ignite.Application.DTOs.Response;
using ignite.Infrastructure.Adapters;
using ignite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Services.Queries
{
    public class CategoryQueryService
    {
        private readonly AppDbContext _context;

        public CategoryQueryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.Where(c => c.DeletedAt == DateTime.MinValue).ToListAsync();

            return categories.Select(category => CategoryAdapter.ToDto(category)!);
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _context
                .Categories.Where(c => c.DeletedAt == DateTime.MinValue && c.Id == id)
                .FirstOrDefaultAsync();

            return CategoryAdapter.ToDto(category);
        }
    }
}
