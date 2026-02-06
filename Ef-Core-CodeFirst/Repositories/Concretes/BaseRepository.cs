using Ef_Core_CodeFirst.Datas;
using Ef_Core_CodeFirst.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Ef_Core_CodeFirst.Repositories.Concretes
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _table;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
            => await _table.ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await _table.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
