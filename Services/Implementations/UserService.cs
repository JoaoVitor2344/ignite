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

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(CreateUserDto dto)
        {
            // Validate the DTO
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new ArgumentException("Name, Email, and Password are required fields.");
            }

            // Check if a user with the same email already exists
            var existingUser = await _repository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            // Create a new user entity from the DTO
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                CreatedAt = DateTime.UtcNow
            };

            // Add the user to the repository
            await _repository.AddAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserDto user)
        {
            // Validate the DTO
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("Name, Email, and Password are required fields.");
            }

            // Check if the user with the specified ID exists in the repository
            var existingUser = await _repository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Update the existing user properties
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.UtcNow;

            // Update the user in the repository
            await _repository.UpdateAsync(existingUser);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            // Check if the user with the specified ID exists in the repository
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.DeletedAt = DateTime.UtcNow; // Soft delete

            // Update the user in the repository
            await _repository.DeleteAsync(user);
        }
    }
}
