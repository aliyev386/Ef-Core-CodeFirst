using Ef_Core_CodeFirst.Models.Entities.Abstracts;

namespace Ef_Core_CodeFirst.Models.Entities.Concretes
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}