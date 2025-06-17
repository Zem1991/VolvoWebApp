using VolvoWebApp.Data.Entities;

namespace VolvoWebApp.Repositories
{
    public interface IVehiclesRepository : IBaseRepository<Vehicle>
    {
        Task<List<Vehicle>> GetByChassisId(string chassisSeries, uint chassisNumber);
    }
}