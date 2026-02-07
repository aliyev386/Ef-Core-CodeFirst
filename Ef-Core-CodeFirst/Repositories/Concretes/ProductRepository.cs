using Ef_Core_CodeFirst.Datas;
using Ef_Core_CodeFirst.Models.Entities.Concretes;
using Ef_Core_CodeFirst.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Ef_Core_CodeFirst.Repositories.Concretes
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }
        public async Task<Product> GetProductByIdWithCategoryAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
