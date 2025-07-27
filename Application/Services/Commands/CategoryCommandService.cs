using ignite.Application.DTOs.Commands.Category;
using ignite.Application.DTOs.Response;
using ignite.Application.Services.Handlers;

namespace ignite.Application.Services.Commands
{
    public class CategoryCommandService
    {
        private readonly CategoryHandlerService _handler;

        public CategoryCommandService(CategoryHandlerService handler)
        {
            _handler = handler;
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryCommand command)
        {
            return await _handler.HandleAsync(command);
        }

        public async Task<CategoryResponseDto?> UpdateCategoryAsync(Guid id, UpdateCategoryCommand command)
        {
            return await _handler.HandleAsync(id, command);
        }

        public async Task<bool> DeleteCategoryAsync(DeleteCategoryCommand command)
        {
            return await _handler.HandleAsync(command);
        }
    }
}
