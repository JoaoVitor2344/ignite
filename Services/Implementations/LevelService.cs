using ignite.Domain.Entities;
using ignite.DTOs;
using ignite.Infrastructure.Repositories;
using ignite.Services.Interfaces;

namespace ignite.Services.Implementations
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _repository;

        public LevelService(ILevelRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Level>> GetLevelsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Level?> GetLevelByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Level> CreateLevelAsync(LevelDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Min < 0 || dto.Max < 0)
            {
                throw new ArgumentException("Name is required", nameof(dto.Name));
            }

            var level = new Level
            {
                Name = dto.Name,
                Min = dto.Min,
                Max = dto.Max,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(level);
            return level;
        }

        public async Task UpdateLevelAsync(Guid id, LevelDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Min < 0 || dto.Max < 0)
            {
                throw new ArgumentException("Name is required", nameof(dto.Name));
            }

            var level = await _repository.GetByIdAsync(id);
            if (level == null)
            {
                throw new KeyNotFoundException($"Level with ID {id} not found.");
            }

            level.Name = dto.Name;
            level.Min = dto.Min;
            level.Max = dto.Max;
            level.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(level);
        }

        public async Task DeleteLevelAsync(Guid id)
        {
            var level = await _repository.GetByIdAsync(id);
            if (level == null)
            {
                throw new KeyNotFoundException($"Level with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}