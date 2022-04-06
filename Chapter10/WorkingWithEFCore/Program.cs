using WorkingWithEFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine($"Using db provider {ProjectConstants.configuredDb}");
//QueryCategoriesProducts();
//QueryCategoriesProductsFilter();
//QueringProducts();
DeleteProducts();

void QueryCategoriesProducts()
{
    using (Northwind db = new())
    {
        IQueryable<Category>? categories = db.Categories?.Include(c => c.Products);
        foreach (Category c in categories)
        {
            Console.WriteLine($"Category {c.CategoryName} has {c.Products.Count} products");
        }
    }
}

void QueryCategoriesProductsFilter()
{
    using (Northwind db = new())
    {
        Console.Write("Enter minimum units in stocks: ");
        string unitsInStock = Console.ReadLine() ?? "10";
        int stock = int.Parse(unitsInStock);
        IQueryable<Category>? categories = db.Categories?.Include(c => c.Products.Where(p => p.Stock >= stock));
        if (categories is null)
        {
            Console.WriteLine("No categories are found.");
            return;
        }
        foreach (Category c in categories)
        {
            Console.WriteLine($"Category {c.CategoryName} has {c.Products.Count} products");
            foreach (Product p in c.Products)
            {
                Console.WriteLine($"\tProduct {p.ProductName} has {p.Stock} units in stock");
            }
        }
    }
}

void QueringProducts()
{
    using (Northwind db = new())
    {
        ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(new ConsoleLoggerProvider());

        int stock = 0;
        bool isValid = false;
        while (!isValid)
        {
            Console.Write($"Please input minimum number in stock:");
            string unitsInStock = Console.ReadLine() ?? "";
            isValid = int.TryParse(unitsInStock, out stock);
        }

        IQueryable<Product>? products = db.Products?.Where(p => p.Stock >= stock).OrderByDescending(p => p.Cost);
        Console.WriteLine(products.ToQueryString());

        if (products is null)
        {
            Console.WriteLine("No products are found.");
            return;
        }

        foreach (Product p in products)
        {
            Console.WriteLine($"\tProduct {p.ProductName}, {p.ProductID} with cost {p.Cost:c} has {p.Stock} units in stock");
        }
    }
}

int DeleteProducts()
{
    string input = "Tigers";
    using (Northwind db = new())
    {
        using (IDbContextTransaction t = db.Database.BeginTransaction())
        {
            Console.WriteLine("Transaction isolation level: {0}", t.GetDbTransaction().IsolationLevel);
            IQueryable<Product>? products = db.Products?.Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));
            if (products is null)
            {
                Console.WriteLine("No products is found");
                return 0;
            }
            db.Products!.RemoveRange(products);
            int affected = db.SaveChanges();
            t.Commit();
            return affected;
        }
    }
}