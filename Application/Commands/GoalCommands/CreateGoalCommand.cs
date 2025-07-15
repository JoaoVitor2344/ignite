using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ignite.Application.Commands.GoalCommands
{
    public class CreateGoalCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
