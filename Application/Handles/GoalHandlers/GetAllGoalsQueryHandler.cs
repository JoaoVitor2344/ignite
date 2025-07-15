using ignite.Application.Queries.GoalQueries;
using ignite.DTOs;
using ignite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Handles.GoalHandlers
{
    public class GetAllGoalsQueryHandler
    {
        private readonly AppDbContext _context;

        public GetAllGoalsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GoalResponseDto>> Handle(GetAllGoalsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Goals
                .AsNoTracking()
                .Select(goal => new GoalResponseDto
                {
                    Id = goal.Id,
                    Name = goal.Name,
                    Description = goal.Description
                })
                .ToListAsync(cancellationToken);
        }
    }
}
