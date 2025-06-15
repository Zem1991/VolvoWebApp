using VolvoWebApp.Dtos;
using VolvoWebApp.Models;

namespace VolvoWebApp.Services
{
    public interface IVehiclesService : IBaseService<Vehicle, VehicleReadDTO, VehicleCreateDTO, VehicleUpdateDTO>
    {
        Task<IEnumerable<VehicleReadDTO>> GetByChassisId(string chassisSeries, uint chassisNumber);
    }
}