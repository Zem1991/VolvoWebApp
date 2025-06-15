using Microsoft.EntityFrameworkCore;
using VolvoWebApp.Data;
using VolvoWebApp.Models;

namespace VolvoWebApp.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseRecord
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entities;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _entities
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await _entities
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public virtual async Task<bool> InsertAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
