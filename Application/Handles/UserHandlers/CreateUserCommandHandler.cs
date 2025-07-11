using ignite.Application.Commands.UserCommands;
using ignite.Domain.Entities;
using ignite.Infrastructure.Data;
using ignite.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Handlers.UserHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasherService _passwordHasher;

    public CreateUserCommandHandler(AppDbContext context, IPasswordHasherService passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Um usuário com este e-mail já existe.");
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = _passwordHasher.HashPassword(request.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}