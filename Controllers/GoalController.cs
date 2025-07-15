using ignite.Application.Commands.GoalCommands;
using ignite.DTOs;
using ignite.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace ignite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalController(GoalService goalService) : ControllerBase
    {
        private readonly GoalService _goalService = goalService;
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GoalResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoals()
        {
            var goals = await _goalService.GetGoalsAsync();
            return Ok(goals);
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GoalResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGoalById(Guid id)
        {
            var goal = await _goalService.GetGoalByIdAsync(id);
            return goal != null ? Ok(goal) : NotFound();
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateGoal([FromBody] CreateGoalCommand command)
        {
            try
            {
                var goalId = await _goalService.CreateGoalAsync(command);
                return CreatedAtAction(nameof(GetGoalById), new { id = goalId }, new { id = goalId });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
        
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] UpdateGoalCommand command)
        {
            try
            {
                command.Id = id;
                await _goalService.UpdateGoalAsync(command);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGoal(Guid id)
        {
            try
            {
                await _goalService.DeleteGoalAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
