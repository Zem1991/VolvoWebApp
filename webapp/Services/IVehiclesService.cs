using VolvoWebApp.Data.Entities;
using VolvoWebApp.Dtos;

namespace VolvoWebApp.Services
{
    public interface IVehiclesService : IBaseService<Vehicle, VehicleReadDTO, VehicleCreateDTO, VehicleUpdateDTO>
    {
        Task<IEnumerable<VehicleReadDTO>> GetByChassisId(string chassisSeries, uint chassisNumber);
    }
}