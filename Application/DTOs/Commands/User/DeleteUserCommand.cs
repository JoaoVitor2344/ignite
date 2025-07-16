using System.ComponentModel.DataAnnotations;

namespace julius.Application.DTOs.Commands.User;

public class DeleteUserCommand
{
    public Guid Id { get; set; }
} 