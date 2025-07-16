using Microsoft.AspNetCore.Mvc;
using julius.Application.Services.Commands;
using julius.Application.DTOs.Commands.Auth;
using julius.Application.DTOs.Response;

namespace julius.Presentation.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthCommandService _authCommandService;

    public AuthController(AuthCommandService authCommandService)
    {
        _authCommandService = authCommandService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _authCommandService.LoginAsync(loginRequest);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}