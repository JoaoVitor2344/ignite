using ignite.DTOs;
using MediatR;

namespace ignite.Application.Queries.GoalQueries
{
    public class GetAllGoalsQuery : IRequest<IEnumerable<GoalResponseDto>>
    {
    }
}
