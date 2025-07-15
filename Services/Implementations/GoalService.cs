using ignite.Application.Commands.GoalCommands;
using ignite.Application.Queries.GoalQueries;
using ignite.DTOs;
using ignite.Services.Interfaces;
using MediatR;

namespace ignite.Services.Implementations
{
    public class GoalService
    {
        private readonly IMediator _mediator;

        public GoalService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<GoalResponseDto>> GetGoalsAsync()
        {
            var query = new GetAllGoalsQuery();
            return await _mediator.Send(query);
        }

        public async Task<GoalResponseDto?> GetGoalByIdAsync(Guid id)
        {
            var query = new GetGoalByIdQuery { Id = id };
            return await _mediator.Send(query);
        }

        public async Task<Guid> CreateGoalAsync(CreateGoalCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task UpdateGoalAsync(UpdateGoalCommand command)
        {
            await _mediator.Send(command);
        }

        public async Task DeleteGoalAsync(Guid id)
        {
            var command = new DeleteGoalCommand { Id = id };
            await _mediator.Send(command);
        }

    }
}
