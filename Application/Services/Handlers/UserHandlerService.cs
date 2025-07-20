using ignite.Infrastructure.Adapters;
using ignite.Application.DTOs.Commands.User;
using ignite.Application.DTOs.Response;
using ignite.Infrastructure.Data;
using ignite.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ignite.Domain.Models;

namespace ignite.Application.Services.Handlers;

public class UserHandlerService
{
    private readonly AppDbContext _context;
    private readonly IPasswordService _passwordService;

    public UserHandlerService(AppDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task<UserResponseDto> HandleAsync(CreateUserCommand command)
    {
        var existingUser = await _context.Users
            .Where(u => u.DeletedAt == null && u.Email == command.Email)
            .FirstOrDefaultAsync();

        if (existingUser != null)
            throw new InvalidOperationException("Email já está em uso");

        var hashedPassword = _passwordService.HashPassword(command.Password);
        
        var user = new User
        {
            Name = command.Name,
            Email = command.Email,
            Password = hashedPassword
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return UserAdapter.ToDto(user)!;
    }

    public async Task<UserResponseDto?> HandleAsync(Guid id, UpdateUserCommand command)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(g => g.Id == id && g.DeletedAt == null);

        if (user == null)
            return null;

        if (!string.IsNullOrEmpty(command.Email) && user.Email != command.Email)
        {
            var existingUser = await _context.Users
                .Where(u => u.DeletedAt == null && u.Email == command.Email)
                .FirstOrDefaultAsync();

            if (existingUser != null)
                throw new InvalidOperationException("Email já está em uso");
        }

        if (!string.IsNullOrEmpty(command.Name))
            user.Name = command.Name;

        if (!string.IsNullOrEmpty(command.Email))
            user.Email = command.Email;

        if (!string.IsNullOrEmpty(command.Password))
            user.Password = _passwordService.HashPassword(command.Password);

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return UserAdapter.ToDto(user)!;
    }

    public async Task<bool> HandleAsync(DeleteUserCommand command)
    {
        var user = await _context.Users
            .Where(u => u.DeletedAt == null && u.Id == command.Id)
            .FirstOrDefaultAsync();

        if (user == null)
            return false;

        user.DeletedAt = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return true;
    }
} 