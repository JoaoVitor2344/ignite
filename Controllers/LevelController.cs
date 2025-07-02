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
        public async Task<IActionResult> GetLevels()
        {
            var levels = await _levelService.GetLevelsAsync();
            return Ok(levels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLevel([FromBody] CreateLevelDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var level = await _levelService.CreateLevelAsync(dto);
            return CreatedAtAction(nameof(GetLevels), new { id = level.Id }, level);
        }
    }
}