using Ef_Core_CodeFirst.Models.Entities.Concretes;

namespace Ef_Core_CodeFirst.Repositories.Abstract
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetAllOrdersWithProduct();
        Task<Order> GetOrderByIdWithProducts(int id);
    }
}
