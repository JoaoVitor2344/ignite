using ignite.Application.DTOs.Response;
using ignite.Domain.Entities;

namespace ignite.Infrastructure.Adapters;

public static class UserAdapter
{
    public static UserResponseDto? ToDto(User? user)
    {
        if (user == null)
        {
            return null;
        }

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
} 