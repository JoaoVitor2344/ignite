
using ignite.DTOs;
using ignite.Domain.Entities;
using ignite.Infrastructure.Repositories;
using ignite.Services.Interfaces;


namespace ignite.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Category> CreateCategoryAsync(CategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Description))
        {
            throw new ArgumentException("Name and Description are required fields.");
        }

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(category);
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Guid id, CategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Description))
        {
            throw new ArgumentException("Name and Description are required fields.");
        }

        var existingCategory = await _repository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException("Category not found");
        }

        existingCategory.Name = dto.Name;
        existingCategory.Description = dto.Description;
        existingCategory.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existingCategory);
        return existingCategory;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            throw new KeyNotFoundException("Category not found");
        }

        category.DeletedAt = DateTime.UtcNow;
        await _repository.DeleteAsync(id);
        return true;
    }
}