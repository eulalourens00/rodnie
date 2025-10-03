using Rodnie.API.DTO.Requests.Pins;
using Rodnie.API.DTO.Responses.Pins;

namespace Rodnie.API.Services
{
    public interface IPinService
    {
        Task<PinResponse> CreateAsync(CreatePinRequest request, Guid userId);
        Task<List<PinResponse>> GetByUserIdAsync(Guid userId);
        Task<PinResponse> GetByIdAsync(Guid id, Guid userId);
        Task<PinResponse> UpdateAsync(Guid id, UpdatePinRequest request, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}