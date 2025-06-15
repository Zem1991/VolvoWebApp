using AutoMapper;
using VolvoWebApp.Dtos;
using VolvoWebApp.Models;
using VolvoWebApp.Repositories;

namespace VolvoWebApp.Services
{
    public class VehiclesService : BaseService<Vehicle, VehicleReadDTO, VehicleCreateDTO, VehicleUpdateDTO>, IVehiclesService
    {
        private readonly IVehiclesRepository _repository;

        public VehiclesService(IMapper mapper, IVehiclesRepository repository) : base(mapper, repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<VehicleReadDTO>> GetByChassisId(string chassisSeries, uint chassisNumber)
        {
            IEnumerable<Vehicle> result = await _repository.GetByChassisId(chassisSeries, chassisNumber);
            return _mapper.Map<IEnumerable<VehicleReadDTO>>(result);
        }
    }
}
