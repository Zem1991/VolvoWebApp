using Microsoft.EntityFrameworkCore;
using VolvoWebApp.Data;
using VolvoWebApp.Data.Entities;

namespace VolvoWebApp.Repositories
{
    public class VehiclesRepository : BaseRepository<Vehicle>, IVehiclesRepository
    {
        public VehiclesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Vehicle>> GetByChassisId(string chassisSeries, uint chassisNumber)
        {
            if (chassisSeries is null)
                throw new ArgumentNullException(nameof(chassisSeries));
            //if (chassisNumber is null)
            //    throw new ArgumentNullException(nameof(chassisNumber));
            List<Vehicle> result = await _entities
                .AsNoTracking()
                .Where(x =>
                    (string.IsNullOrEmpty(chassisSeries) || x.ChassisSeries.Equals(chassisSeries))
                    && (string.IsNullOrEmpty($"{chassisNumber}") || x.ChassisNumber == chassisNumber))
                .ToListAsync();
            return result;
        }
    }
}
