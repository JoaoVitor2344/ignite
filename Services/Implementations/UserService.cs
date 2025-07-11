using MediatR;
using ignite.Application.Commands.UserCommands;
using ignite.Application.Queries.UserQueries;
using ignite.DTOs;
using ignite.Services.Interfaces;

namespace ignite.Services.Implementations;

public class UserService : IUserService
{
    private readonly IMediator _mediator;

    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
    {
        var query = new GetAllUsersQuery();
        return await _mediator.Send(query);
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
    {
        var query = new GetUserByIdQuery { Id = id };
        return await _mediator.Send(query);
    }

    public async Task<Guid> CreateUserAsync(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task UpdateUserAsync(UpdateUserCommand command)
    {
        await _mediator.Send(command);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var command = new DeleteUserCommand { Id = id };
        await _mediator.Send(command);
    }
}