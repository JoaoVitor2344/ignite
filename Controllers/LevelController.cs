using ignite.Domain.Entities;
using ignite.DTOs;
using ignite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ignite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LevelController : ControllerBase
    {
        private readonly ILevelService _levelService;

        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpGet]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Level>>> GetLevels()
        {
            try
            {
                var levels = await _levelService.GetLevelsAsync();
                return Ok(levels);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [HttpGet("{id}")]
        public async Task<ActionResult<Level?>> GetLevelById(Guid id)
        {
            try
            {
                var level = await _levelService.GetLevelByIdAsync(id);
                if (level == null)
                {
                    return NotFound();
                }
                return Ok(level);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Level>> CreateLevel([FromBody] LevelDto dto)
        {
            try
            {
                var level = await _levelService.CreateLevelAsync(dto);
                return Ok(level);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLevel(Guid id, [FromBody] LevelDto dto)
        {
            try
            {
                await _levelService.UpdateLevelAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevel(Guid id)
        {
            try
            {
                await _levelService.DeleteLevelAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}