using ignite.Application.Commands.UserCommands;
using ignite.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Handlers.UserHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly AppDbContext _context;
    // injete seu IPasswordHasherService aqui também!

    public UpdateUserCommandHandler(AppDbContext context /*, IPasswordHasherService passwordHasher */)
    {
        _context = context;
        // _passwordHasher = passwordHasher; // Lembre-se de injetar o hasher se necessário
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        user.Name = request.Name;
        user.Email = request.Email;

        if (!string.IsNullOrEmpty(request.Password))
        {
            // user.Password = _passwordHasher.HashPassword(request.Password);
            user.Password = request.Password; // Placeholder - substitua pelo hasher
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}