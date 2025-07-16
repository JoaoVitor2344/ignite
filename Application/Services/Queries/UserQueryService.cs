using julius.Application.DTOs.Response;
using julius.Infrastructure.Data;
using julius.Infrastructure.Adapters;
using Microsoft.EntityFrameworkCore;

namespace julius.Application.Services.Queries;

public class UserQueryService
{
    private readonly AppDbContext _context;

    public UserQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _context.Users
            .Where(u => u.DeletedAt == null)
            .ToListAsync();

        return users.Select(user => UserAdapter.ToDto(user)!);
    }

    public async Task<UserDTO?> GetUserByIdAsync(Guid id)
    {
        var user = await _context.Users
            .Where(u => u.DeletedAt == null && u.Id == id)
            .FirstOrDefaultAsync();

        return UserAdapter.ToDto(user);
    }

    public async Task<UserDTO?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
            .Where(u => u.DeletedAt == null && u.Email == email)
            .FirstOrDefaultAsync();

        return UserAdapter.ToDto(user);
    }
} 