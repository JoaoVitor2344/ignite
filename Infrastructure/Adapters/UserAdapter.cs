using ignite.Application.DTOs.Response;
using ignite.Domain.Models;

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
            Id = user.Id, // Ensure user.Id is of type Guid
            Name = user.Name,
            Email = user.Email
        };
    }
} 