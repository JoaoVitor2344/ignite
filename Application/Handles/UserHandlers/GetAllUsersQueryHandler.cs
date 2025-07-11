using ignite.Application.Queries.UserQueries;
using ignite.DTOs;
using ignite.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Handlers.UserHandlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponseDto>>
{
    private readonly AppDbContext _context;

    public GetAllUsersQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking() // Otimização de performance para consultas de leitura
            .Select(user => new UserResponseDto // Mapeamento para DTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            })
            .ToListAsync(cancellationToken);
    }
}