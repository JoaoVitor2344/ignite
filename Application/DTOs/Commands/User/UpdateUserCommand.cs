using System.ComponentModel.DataAnnotations;

namespace ignite.Application.DTOs.Commands.User;

public class UpdateUserCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
} 