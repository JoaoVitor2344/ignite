using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ignite.Application.Commands.UserCommands;

public class UpdateUserCommand : IRequest
{
    public Guid Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    // A senha é opcional na atualização
    [MinLength(8)]
    public string? Password { get; set; }
}