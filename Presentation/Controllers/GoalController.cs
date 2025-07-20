using ignite.Application.DTOs.Commands.Goal;
using ignite.Application.Services.Commands;
using ignite.Application.Services.Queries;
using ignite.Application.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ignite.Presentation.Controllers;

[ApiController]
[Route("api/v1/goals")]
public class GoalController : ControllerBase
{
    private readonly GoalQueryService _goalQueryService;
    private readonly GoalCommandService _goalCommandService;

    public GoalController(GoalQueryService goalQueryService, GoalCommandService goalCommandService)
    {
        _goalQueryService = goalQueryService;
        _goalCommandService = goalCommandService;
    }

    [Authorize]
    [HttpGet("consultar")]
    [ProducesResponseType(typeof(List<GoalResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllGoals()
    {
        var goals = await _goalQueryService.GetAllGoalsAsync();
        return Ok(goals);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GoalResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGoalById(Guid id)
    {
        var goal = await _goalQueryService.GetGoalByIdAsync(id);
        if (goal == null)
        {
            return NotFound(new { message = "Meta não encontrada." });
        }
        return Ok(goal);
    }

    [HttpPost("criar")]
    [Authorize]
    [ProducesResponseType(typeof(GoalResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGoal([FromBody] CreateGoalCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdGoal = await _goalCommandService.CreateGoalAsync(command);
        return CreatedAtAction(nameof(GetGoalById), new { id = createdGoal.Id }, createdGoal);
    }

    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(GoalResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] UpdateGoalCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        var updatedGoal = await _goalCommandService.UpdateGoalAsync(id, command);
        if (updatedGoal == null)
        {
            return NotFound(new { message = "Meta não encontrada para atualização." });
        }
        return Ok(updatedGoal);
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGoal(Guid id)
    {
        var command = new DeleteGoalCommand { Id = id };
        var success = await _goalCommandService.DeleteGoalAsync(command);

        if (!success)
        {
            return NotFound(new { message = "Meta não encontrada para exclusão." });
        }

        return Ok("Meta excluída com sucesso.");
    }
} 