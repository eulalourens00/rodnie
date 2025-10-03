using Rodnie.API.Models;

namespace Rodnie.API.Repositories
{
    public interface IPinRepository
    {
        Task<Pin> CreateAsync(Pin pin);

        Task<List<Pin>> GetByUserIdAsync(Guid userId);
        Task<Pin> GetByIdAsync(Guid id);
        Task<Pin> UpdateAsync(Pin pin);
        Task<bool> DeleteAsync(Guid id);

        Task<Pin> GetByIdAndUserIdAsync(Guid id, Guid userId);
    }
}