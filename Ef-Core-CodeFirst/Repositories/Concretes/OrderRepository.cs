using Ef_Core_CodeFirst.Datas;
using Ef_Core_CodeFirst.Models.Entities.Concretes;
using Ef_Core_CodeFirst.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Ef_Core_CodeFirst.Repositories.Concretes
{
    public class OrderRepository: BaseRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<List<Order>> GetAllOrdersWithProduct() => await _context.Orders.Include(x => x.Products).ToListAsync();

        public async Task<Order> GetOrderByIdWithProducts(int id) => await _context.Orders.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }
}
