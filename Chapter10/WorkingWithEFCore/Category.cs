using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingWithEFCore
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Column(TypeName = "ntext")]
        public string? CategoryName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new HashSet<Product>();
        }
    }
}