using ignite.Application.Commands.GoalCommands;
using ignite.Infrastructure.Data;

namespace ignite.Application.Handles.GoalHandlers
{
    public class UpdateGoalCommandHandler
    {
        private readonly AppDbContext _context;
        public UpdateGoalCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
        {
            var goal = await _context.Goals.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (goal == null)
            {
                throw new KeyNotFoundException("Goal not found.");
            }

            goal.Name = request.Name;
            goal.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
