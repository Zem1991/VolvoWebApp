using VolvoWebApp.Data.Entities;

namespace VolvoWebApp.Repositories
{
    public interface IBaseRepository<T> where T : BaseRecord
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}