using ignite.Domain.Entities;
using ignite.DTOs;

namespace ignite.Services.Interfaces
{
    public interface ILevelService
    {
        Task<IEnumerable<Level>> GetLevelsAsync();
        Task<Level?> GetLevelByIdAsync(Guid id);
        Task<Level> CreateLevelAsync(LevelDto dto);
        Task UpdateLevelAsync(Guid id, LevelDto dto);
        Task DeleteLevelAsync(Guid id);
    }
}