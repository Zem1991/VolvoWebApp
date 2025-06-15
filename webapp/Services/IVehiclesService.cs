using VolvoWebApp.Dtos;

namespace VolvoWebApp.Services
{
    public interface IVehiclesService
    {
        Task<IEnumerable<VehicleReadDTO>> GetByChassisId(string chassisSeries, uint chassisNumber);
    }
}