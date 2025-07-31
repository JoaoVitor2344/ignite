using ignite.Application.DTOs.Commands.Level;
using ignite.Application.Services.Commands;
using ignite.Application.Services.Queries;
using Microsoft.AspNetCore.Mvc;
using ignite.Application.DTOs.Response;

namespace ignite.Presentation.Controllers;

[ApiController]
[Route("api/v1/levels")]
public class LevelController : ControllerBase
{
    private readonly LevelCommandService _commandService;
    private readonly LevelQueryService _queryService;
    private readonly ILogger<LevelController> _logger;

    public LevelController(LevelCommandService commandService, LevelQueryService queryService, ILogger<LevelController> logger)
    {
        _commandService = commandService;
        _queryService = queryService;
        _logger = logger;
    }

    [HttpGet("consultar")]
    [ProducesResponseType(typeof(List<LevelResponseDto>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var levels = await _queryService.GetAllLevelsAsync();
        return Ok(levels);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LevelResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var level = await _queryService.GetLevelByIdAsync(id);
        if (level == null)
            return NotFound(new { message = "Level não encontrado." });
        return Ok(level);
    }

    [HttpPost("criar")]
    [ProducesResponseType(typeof(LevelResponseDto), 201)]
    public async Task<IActionResult> Create([FromBody] CreateLevelCommand command)
    {
        var created = await _commandService.CreateLevelAsync(command);
        return Created(string.Empty, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(LevelResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLevelCommand command)
    {
        var updated = await _commandService.UpdateLevelAsync(id, command);
        if (updated == null)
            return NotFound(new { message = "Level não encontrado." });
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _commandService.DeleteLevelAsync(new DeleteLevelCommand { Id = id });
        if (!deleted)
            return NotFound(new { message = "Level não encontrado." });
        return NoContent();
    }
}
