using VolvoWebApp.Dtos;

namespace VolvoWebApp.Services
{
    public interface IVehiclesService
    {
        Task<bool> CreateAsync(VehicleCreateDTO vehicle);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<VehicleReadDTO>> GetAllAsync();
        Task<VehicleReadDTO?> GetByIdAsync(string id);
        Task<bool> UpdateAsync(VehicleUpdateDTO vehicle);
    }
}