using AutoMapper;
using VolvoWebApp.Models;
using VolvoWebApp.Repositories;

namespace VolvoWebApp.Services
{
    public class VehiclesService
    {
        private readonly IMapper _mapper;
        private readonly IVehiclesRepository _repository;

        public VehiclesService(IMapper mapper, IVehiclesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            IEnumerable<Vehicle> result = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleDto>>(result);
        }

        public async Task<VehicleDto?> GetByIdAsync(string id)
        {
            Vehicle? result = await _repository.GetByIdAsync(id);
            return _mapper.Map<VehicleDto>(result);
        }

        public async Task<bool> CreateAsync(VehicleDto vehicle)
        {
            Vehicle entity = _mapper.Map<VehicleDto, Vehicle>(vehicle);
            bool result = await _repository.InsertAsync(entity);
            return result;
        }

        public async Task<bool> UpdateAsync(VehicleDto vehicle)
        {
            Vehicle entity = _mapper.Map<VehicleDto, Vehicle>(vehicle);
            bool result = await _repository.UpdateAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Vehicle? entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            bool result = await _repository.DeleteAsync(entity);
            return result;
        }
    }
}
