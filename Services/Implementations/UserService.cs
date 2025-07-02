using ignite.DTOs;
using ignite.Domain.Entities;
using ignite.Infrastructure.Repositories;
using ignite.Services.Interfaces;

namespace ignite.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };

            await _repository.AddAsync(user);
            return user;
        }
    }
}
