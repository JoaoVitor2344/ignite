using System.ComponentModel.DataAnnotations;

namespace ignite.Application.DTOs.Commands.User;

public class DeleteUserCommand
{
    public Guid Id { get; set; }
} 