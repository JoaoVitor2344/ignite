using ignite.DTOs;
using MediatR;

namespace ignite.Application.Queries.UserQueries;

public class GetAllUsersQuery : IRequest<IEnumerable<UserResponseDto>>
{
}