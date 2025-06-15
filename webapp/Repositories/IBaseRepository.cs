using VolvoWebApp.Models;

namespace VolvoWebApp.Repositories
{
    public interface IBaseRepository<T> where T : BaseRecord
    {
        Task<bool> DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}