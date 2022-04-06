using Packt.Shared;

namespace Northwind.Mvc.Models
{
    public class HomeIndexViewModel
    {
        public int VisitorCount { get; set; }
        public IList<Product> Products { get; set; } = null!;
        public IList<Category> Categories { get; set; } = null!;
    }
}