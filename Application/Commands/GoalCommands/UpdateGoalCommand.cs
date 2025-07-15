using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ignite.Application.Commands.GoalCommands
{
    public class UpdateGoalCommand : IRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
