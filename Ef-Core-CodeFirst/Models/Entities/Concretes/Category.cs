using Ef_Core_CodeFirst.Models.Entities.Abstracts;

namespace Ef_Core_CodeFirst.Models.Entities.Concretes
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
