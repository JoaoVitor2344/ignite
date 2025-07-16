using julius.Application.DTOs.Commands.Auth;
using julius.Application.DTOs.Response;

namespace julius.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO);
        Task<bool> ValidateTokenAsync(string token);
        string GenerateTokenAsync(Guid userId);
    }
} 