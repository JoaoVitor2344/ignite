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

        public async Task<Level> CreateLevelAsync(CreateLevelDto dto)
        {
            var level = new Level
            {
                Name = dto.Name,
                Min = dto.Min,
                Max = dto.Max,
                CreatedAt = dto.CreatedAt
            };

            await _repository.AddAsync(level);
            return level;
        }
    }
}