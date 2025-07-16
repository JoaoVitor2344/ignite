using ignite.Application.DTOs.Response;
using ignite.Infrastructure.Data;
using ignite.Infrastructure.Adapters;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Services.Queries;

public class UserQueryService
{
    private readonly AppDbContext _context;

    public UserQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _context.Users
            .Where(u => u.DeletedAt == null)
            .ToListAsync();

        return users.Select(user => UserAdapter.ToDto(user)!);
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _context.Users
            .Where(u => u.DeletedAt == null && u.Id == id)
            .FirstOrDefaultAsync();

        return UserAdapter.ToDto(user);
    }

    public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
            .Where(u => u.DeletedAt == null && u.Email == email)
            .FirstOrDefaultAsync();

        return UserAdapter.ToDto(user);
    }
} 