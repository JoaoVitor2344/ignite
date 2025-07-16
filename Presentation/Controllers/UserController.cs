using ignite.Application.DTOs.Commands.User;
using ignite.Application.Services.Commands;
using ignite.Application.Services.Queries;
using ignite.Application.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ignite.Presentation.Controllers;

[ApiController]
[Route("api/v1/usuarios")]
public class UserController : ControllerBase
{
    private readonly UserQueryService _userQueryService;
    private readonly UserCommandService _userCommandService;

    public UserController(UserQueryService userQueryService, UserCommandService userCommandService)
    {
        _userQueryService = userQueryService;
        _userCommandService = userCommandService;
    }

    [Authorize]
    [HttpGet("consultar")]
    [ProducesResponseType(typeof(List<UserResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userQueryService.GetAllUsersAsync();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userQueryService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { message = "Usuário não encontrado." });
        }
        return Ok(user);
    }

    [HttpPost("criar")]
    [Authorize]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdUser = await _userCommandService.CreateUserAsync(command);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != command.Id)
        {
            return BadRequest(
                new { message = "O ID da rota não corresponde ao ID no corpo da requisição." }
            );
        }

        try
        {
            var updatedUser = await _userCommandService.UpdateUserAsync(command);
            if (updatedUser == null)
            {
                return NotFound(new { message = "Usuário não encontrado para atualização." });
            }
            return Ok(updatedUser);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUserCommand { Id = id };
        var success = await _userCommandService.DeleteUserAsync(command);

        if (!success)
        {
            return NotFound(new { message = "Usuário não encontrado para exclusão." });
        }

        return Ok("Usuário excluído com sucesso.");
    }
}
