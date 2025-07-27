using ignite.Application.DTOs.Commands.Category;
using ignite.Application.DTOs.Response;
using ignite.Application.Services.Commands;
using ignite.Application.Services.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ignite.Presentation.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryQueryService _categoryQueryService;
    private readonly CategoryCommandService _categoryCommandService;

    public CategoryController(CategoryQueryService categoryQueryService, CategoryCommandService categoryCommandService)
    {
        _categoryQueryService = categoryQueryService;
        _categoryCommandService = categoryCommandService;
    }

    [Authorize]
    [HttpGet("consultar")]
    [ProducesResponseType(typeof(List<CategoryResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryQueryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryQueryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound(new { message = "Categoria não encontrada." });
        }
        return Ok(category);
    }

    [HttpPost("criar")]
    [Authorize]
    [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdCategory = await _categoryCommandService.CreateCategoryAsync(command);
        return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedCategory = await _categoryCommandService.UpdateCategoryAsync(id, command);
        if (updatedCategory == null)
        {
            return NotFound(new { message = "Categoria não encontrada." });
        }
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var command = new DeleteCategoryCommand { Id = id };
        var success = await _categoryCommandService.DeleteCategoryAsync(command);
        if (!success)
        {
            return NotFound(new { message = "Categoria não encontrada." });
        }
        return NoContent();
    }
}
