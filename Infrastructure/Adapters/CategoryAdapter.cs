using ignite.Application.DTOs.Response;
using ignite.Domain.Models;

namespace ignite.Infrastructure.Adapters
{
    public class CategoryAdapter
    {
        public static CategoryResponseDto? ToDto(Category? category)
        {
            if (category == null)
            {
                return null;
            }

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
