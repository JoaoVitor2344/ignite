using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ignite.Application.Commands.UserCommands;

public class CreateUserCommand : IRequest<Guid>
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}