using ignite.Application.Commands.GoalCommands;
using ignite.Domain.Entities;
using ignite.Infrastructure.Data;
using MediatR;

namespace ignite.Application.Handles.GoalHandlers
{
    public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, Guid>
    {
        private readonly AppDbContext _context;

        public CreateGoalCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
        {
            var existingGoal = _context.Goals
                .FirstOrDefault(g => g.Name == request.Name && g.Id == request.Id);

            if (existingGoal != null)
            {
                return existingGoal.Id;
            }

            var newGoal = new Goal
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Goals.AddAsync(newGoal, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newGoal.Id;
        }
    }
}
