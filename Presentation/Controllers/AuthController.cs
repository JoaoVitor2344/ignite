using Microsoft.AspNetCore.Mvc;
using ignite.Application.Services.Commands;
using ignite.Application.DTOs.Commands.Auth;
using ignite.Application.DTOs.Response;

namespace ignite.Presentation.Controllers;

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
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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