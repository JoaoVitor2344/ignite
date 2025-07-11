using MediatR;

namespace ignite.Application.Commands.UserCommands;

public class DeleteUserCommand : IRequest
{
    public Guid Id { get; set; }
}