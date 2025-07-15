using ignite.DTOs;
using MediatR;

namespace ignite.Application.Queries.GoalQueries
{
    public class GetGoalByIdQuery : IRequest<GoalResponseDto?>
    {
        public Guid Id { get; set; }
    }
}
