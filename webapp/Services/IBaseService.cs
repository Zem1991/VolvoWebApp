using VolvoWebApp.Data.Entities;

namespace VolvoWebApp.Services
{
    public interface IBaseService<Data, ReadDto, CreateDto, UpdateDto> where Data : BaseRecord
    {
        Task<IEnumerable<ReadDto>> GetAllAsync();
        Task<ReadDto?> GetByIdAsync(string id);
        Task<bool> CreateAsync(CreateDto vehicle);
        Task<bool> UpdateAsync(UpdateDto vehicle);
        Task<bool> DeleteAsync(string id);
    }
}