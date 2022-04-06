using Microsoft.EntityFrameworkCore;

namespace LinqWithEFCore
{
    public class Northwind : DbContext
    {
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=true;MultipleActiveResultSets=true;";
            optionsBuilder.UseSqlServer(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.UnitPrice).HasConversion<double>();
        }
    }
}