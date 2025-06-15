using AutoMapper;
using VolvoWebApp.Dtos;
using VolvoWebApp.Models;
using VolvoWebApp.Repositories;

namespace VolvoWebApp.Services
{
    public class VehiclesService : BaseService<Vehicle, VehicleReadDTO, VehicleCreateDTO, VehicleUpdateDTO>, IVehiclesService
    {
        public VehiclesService(IMapper mapper, IVehiclesRepository repository) : base(mapper, repository)
        {
        }

        public async Task<IEnumerable<Vehicle>> GetByChassisId(string chassisSeries, uint chassisNumber)
        {
            IEnumerable<Vehicle> result = await ((IVehiclesRepository)_repository).GetByChassisId(chassisSeries, chassisNumber);
            return (IEnumerable<Vehicle>)_mapper.Map<IEnumerable<VehicleReadDTO>>(result);
        }
    }
}
