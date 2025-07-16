using ignite.Infrastructure.Adapters;
using ignite.Application.DTOs.Response;
using ignite.Application.DTOs.Commands.Auth;
using ignite.Infrastructure.Data;
using ignite.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Services.Handlers;

public class AuthHandlerService
{
    private readonly AppDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly string _jwtSecret;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;

    public AuthHandlerService(AppDbContext context, IPasswordService passwordService, IConfiguration configuration)
    {
        _context = context;
        _passwordService = passwordService;
        _jwtSecret = configuration["Jwt:Secret"] ?? "your-super-secret-key-with-at-least-32-characters";
        _jwtIssuer = configuration["Jwt:Issuer"] ?? "julius";
        _jwtAudience = configuration["Jwt:Audience"] ?? "julius-users";
    }

    public async Task<LoginResponseDto> HandleAsync(LoginCommand loginRequestDTO)
    {
        var user = await _context.Users
            .Where(u => u.DeletedAt == null && u.Email == loginRequestDTO.Email)
            .FirstOrDefaultAsync();

        if (user == null)
            throw new InvalidOperationException("Credenciais inválidas");

        if (!_passwordService.VerifyPassword(loginRequestDTO.Password, user.Password))
            throw new InvalidOperationException("Credenciais inválidas");

        var token = GenerateToken(user.Id);
        // O UserAdapter pode ser ajustado para retornar UserResponseDto, mas LoginResponseDto espera Email e Name
        return new LoginResponseDto {
            Token = token,
            Email = user.Email,
            UserId = user.Id,
            Expiration = DateTime.UtcNow.AddHours(24)
        };
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtIssuer,
                ValidateAudience = true,
                ValidAudience = _jwtAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public string GenerateToken(Guid userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            Issuer = _jwtIssuer,
            Audience = _jwtAudience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
} 