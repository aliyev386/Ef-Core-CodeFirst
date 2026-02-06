using Ef_Core_CodeFirst.Datas;
using Ef_Core_CodeFirst.Models.Entities.Concretes;
using Ef_Core_CodeFirst.Repositories.Abstract;

namespace Ef_Core_CodeFirst.Repositories.Concretes
{
    public class OrderRepository: BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context)
        : base(context) { }
    }
}
