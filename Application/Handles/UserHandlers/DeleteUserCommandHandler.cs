using ignite.Application.Commands.UserCommands;
using ignite.Infrastructure.Data;
using MediatR;

namespace ignite.Application.Handlers.UserHandlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly AppDbContext _context;

    public DeleteUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new KeyNotFoundException("Usuário não encontrado.");
        user.SoftDelete();
        await _context.SaveChangesAsync(cancellationToken);
    }
}