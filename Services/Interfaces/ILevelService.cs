using ignite.Domain.Entities;
using ignite.DTOs;

namespace ignite.Services.Interfaces
{
    public interface ILevelService
    {
        Task<IEnumerable<Level>> GetLevelsAsync();
        Task<Level> CreateLevelAsync(CreateLevelDto dto);
    }
}