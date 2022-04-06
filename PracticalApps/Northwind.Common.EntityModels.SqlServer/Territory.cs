using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    public partial class Territory
    {
        public Territory()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [Column("TerritoryID")]
        [Required]
        [StringLength(20)]
        public string TerritoryId { get; set; } = null!;
        [StringLength(50)]
        [Required]
        public string TerritoryDescription { get; set; } = null!;
        [Column("RegionID")]
        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        [InverseProperty("Territories")]
        public virtual Region Region { get; set; } = null!;

        [ForeignKey("TerritoryId")]
        [InverseProperty("Territories")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
