using VolvoWebApp.Models;

namespace VolvoWebApp.Repositories
{
    public interface IVehiclesRepository
    {
        Task<List<Vehicle>> GetByChassisId(string chassisSeries, uint chassisNumber);
    }
}