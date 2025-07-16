using julius.Application.DTOs.Commands.Auth;
using julius.Application.DTOs.Response;
using julius.Application.Services.Handlers;

namespace julius.Application.Services.Commands;

public class AuthCommandService
{
    private readonly AuthHandlerService _handler;
    
    public AuthCommandService(AuthHandlerService handler)
    {
        _handler = handler;
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
    {
        return await _handler.HandleAsync(loginRequestDTO);
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        return _handler.ValidateToken(token);
    }

    public string GenerateTokenAsync(Guid userId)
    {
        return _handler.GenerateToken(userId);
    }
} 