using MediatR;

namespace ignite.Application.Commands.GoalCommands
{
    public class DeleteGoalCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
