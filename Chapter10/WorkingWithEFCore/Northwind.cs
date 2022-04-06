using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore
{
    public class Northwind : DbContext
    {
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ProjectConstants.configuredDb == ProjectConstants.DBProvider.SQLite)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Northwind.db");
                optionsBuilder.UseSqlite(path);
            }
            else if (ProjectConstants.configuredDb == ProjectConstants.DBProvider.SQLServer)
            {
                string connection = "Data Source=.;Initial Catalog=Northwind;Integrated Security=true;MultipleActiveResultSets=true;";
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .Property(p => p.CategoryName)
            .IsRequired()
            .HasMaxLength(15);
            if (ProjectConstants.configuredDb == ProjectConstants.DBProvider.SQLite)
            {
                modelBuilder.Entity<Product>()
                .Property(p => p.Cost)
                .HasConversion<double>();
            }
        }
    }
}