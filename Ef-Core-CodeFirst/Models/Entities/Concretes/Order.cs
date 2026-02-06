using Ef_Core_CodeFirst.Models.Entities.Abstracts;

namespace Ef_Core_CodeFirst.Models.Entities.Concretes
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }
    }
}
