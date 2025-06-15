using VolvoWebApp.Models;

namespace VolvoWebApp.Services
{
    public interface IVehiclesService
    {
        Task<IEnumerable<Vehicle>> GetByChassisId(string chassisSeries, uint chassisNumber);
    }
}