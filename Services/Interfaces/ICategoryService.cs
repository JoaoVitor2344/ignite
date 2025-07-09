using ignite.Domain.Entities;
using ignite.DTOs;

namespace ignite.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<Category> CreateCategoryAsync(CategoryDto dto);
    Task<Category> UpdateCategoryAsync(Guid id, CategoryDto dto);
    Task<bool> DeleteCategoryAsync(Guid id);
}