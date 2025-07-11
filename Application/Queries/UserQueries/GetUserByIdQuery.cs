using ignite.DTOs;
using MediatR;

namespace ignite.Application.Queries.UserQueries;

// O retorno pode ser nulo se o usuário não for encontrado.
public class GetUserByIdQuery : IRequest<UserResponseDto?>
{
    public Guid Id { get; set; }
}