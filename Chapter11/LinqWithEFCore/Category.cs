using System.ComponentModel.DataAnnotations;

namespace LinqWithEFCore
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
    }
}