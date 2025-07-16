using ignite.Application.DTOs.Response;
using ignite.Application.DTOs.Commands.Auth;

namespace ignite.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginCommand loginRequestDTO);
        Task<bool> ValidateTokenAsync(string token);
        string GenerateTokenAsync(Guid userId);
    }
}
