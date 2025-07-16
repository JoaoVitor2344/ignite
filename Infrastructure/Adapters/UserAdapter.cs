using julius.Application.DTOs.Response;
using julius.Domain.Models;

namespace julius.Infrastructure.Adapters;

public static class UserAdapter
{
    public static UserDTO? ToDto(User? user)
    {
        if (user == null)
        {
            return null;
        }

        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt.ToString("o"),
            UpdatedAt = user.UpdatedAt.ToString("o")
        };
    }
} 