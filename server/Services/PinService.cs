using AutoMapper;
using Rodnie.API.Models;
using Rodnie.API.DTO.Requests.Pins;
using Rodnie.API.DTO.Responses.Pins;
using Rodnie.API.Repositories;

namespace Rodnie.API.Services
{
    public class PinService : IPinService
    {
        private readonly IPinRepository _repository;
        private readonly IMapper _mapper;

        public PinService(IPinRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PinResponse> CreateAsync(CreatePinRequest request, Guid userId)
        {
            var pin = _mapper.Map<Pin>(request);
            pin.id = Guid.NewGuid();
            pin.owner_user_id = userId;
            pin.created_at = DateTime.UtcNow;
            pin.updated_at = DateTime.UtcNow;

            var createdPin = await _repository.CreateAsync(pin);
            return _mapper.Map<PinResponse>(createdPin);
        }

        public async Task<List<PinResponse>> GetByUserIdAsync(Guid userId)
        {
            var pins = await _repository.GetByUserIdAsync(userId);
            return _mapper.Map<List<PinResponse>>(pins);
        }

        public async Task<PinResponse> GetByIdAsync(Guid id, Guid userId)
        {
            var pin = await _repository.GetByIdAndUserIdAsync(id, userId);
            if (pin == null)
                return null;

            return _mapper.Map<PinResponse>(pin);
        }

        public async Task<PinResponse> UpdateAsync(Guid id, UpdatePinRequest request, Guid userId)
        {
            var pin = await _repository.GetByIdAndUserIdAsync(id, userId);
            if (pin == null)
                return null;

            _mapper.Map(request, pin);
            pin.updated_at = DateTime.UtcNow;

            var updatedPin = await _repository.UpdateAsync(pin);
            return _mapper.Map<PinResponse>(updatedPin);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var pin = await _repository.GetByIdAndUserIdAsync(id, userId);
            if (pin == null)
                return false;

            return await _repository.DeleteAsync(id);
        }
    }
}