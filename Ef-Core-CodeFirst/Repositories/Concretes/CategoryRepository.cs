using Ef_Core_CodeFirst.Datas;
using Ef_Core_CodeFirst.Models.Entities.Concretes;
using Ef_Core_CodeFirst.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Ef_Core_CodeFirst.Repositories.Concretes
{
    public class CategoryRepository : BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Category>> GetAllWithProductsAsync()
        {
            return await _context.Categories
                .Include(p => p.Products)
                .ToListAsync();
        }
    }
}
