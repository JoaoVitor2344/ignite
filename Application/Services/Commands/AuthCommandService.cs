using ignite.Application.DTOs.Response;
using ignite.Application.DTOs.Commands.Auth;
using ignite.Application.Services.Handlers;

namespace ignite.Application.Services.Commands;

public class AuthCommandService
{
    private readonly AuthHandlerService _handler;

    public AuthCommandService(AuthHandlerService handler)
    {
        _handler = handler;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginCommand loginRequestDTO)
    {
        return await _handler.HandleAsync(loginRequestDTO);
    }

    public bool ValidateToken(string token)
    {
        return _handler.ValidateToken(token);
    }

    public string GenerateTokenAsync(Guid userId)
    {
        return _handler.GenerateToken(userId);
    }
}
