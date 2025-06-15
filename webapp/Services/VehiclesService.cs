using AutoMapper;
using VolvoWebApp.Dtos;
using VolvoWebApp.Models;
using VolvoWebApp.Repositories;

namespace VolvoWebApp.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IMapper _mapper;
        private readonly IVehiclesRepository _repository;

        public VehiclesService(IMapper mapper, IVehiclesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<VehicleReadDTO>> GetAllAsync()
        {
            IEnumerable<Vehicle> result = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleReadDTO>>(result);
        }

        public async Task<VehicleReadDTO?> GetByIdAsync(string id)
        {
            Vehicle? result = await _repository.GetByIdAsync(id);
            return _mapper.Map<VehicleReadDTO>(result);
        }

        public async Task<bool> CreateAsync(VehicleCreateDTO vehicle)
        {
            Vehicle entity = _mapper.Map<VehicleCreateDTO, Vehicle>(vehicle);
            bool result = await _repository.InsertAsync(entity);
            return result;
        }

        public async Task<bool> UpdateAsync(VehicleUpdateDTO vehicle)
        {
            Vehicle entity = _mapper.Map<VehicleUpdateDTO, Vehicle>(vehicle);
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
