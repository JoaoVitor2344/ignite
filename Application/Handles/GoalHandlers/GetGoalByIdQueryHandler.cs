using ignite.Application.Queries.GoalQueries;
using ignite.DTOs;
using ignite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ignite.Application.Handles.GoalHandlers
{
    public class GetGoalByIdQueryHandler
    {
        private readonly AppDbContext _context;

        public GetGoalByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GoalResponseDto?> Handle(GetGoalByIdQuery request, CancellationToken cancellationToken)
        {
            var goal = await _context.Goals
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
            
            if (goal == null)
            {
                return null;
            }

            return new GoalResponseDto
            {
                Id = goal.Id,
                Name = goal.Name,
                Description = goal.Description
            };
        }
    }
}
