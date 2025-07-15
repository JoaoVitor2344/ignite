using ignite.Application.Commands.GoalCommands;
using ignite.Infrastructure.Data;
using MediatR;

namespace ignite.Application.Handles.GoalHandlers
{
    public class DeleteGoalCommandHandler : IRequest<DeleteGoalCommandHandler>
    {
        private readonly AppDbContext _context;
        public DeleteGoalCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
        {
            var goal = await _context.Goals.FindAsync(new object[] { request.Id }, cancellationToken) 
                ?? throw new KeyNotFoundException("Usuario não encontrada.");

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
