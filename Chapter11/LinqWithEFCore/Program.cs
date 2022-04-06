using System.Xml;
using System.Xml.Linq;
using LinqWithEFCore;
using static System.Console;

var items = Enumerable.Range(1, 5000);
var parallel = items.AsParallel().WithDegreeOfParallelism(1).Select(x =>
{
    Console.WriteLine(x);
    return x;
});



using (Northwind db = new())
{
    Product[] prods = db.Products.ToArray();
    XElement xml = new("products", from product in prods
                                   select new XElement("product",
                                       new XAttribute("id", product.ProductId),
                                       new XAttribute("price", product.UnitPrice),
                                       new XElement("name", product.ProductName)));
    WriteLine(xml);
}
ReadLine();

using (Northwind n = new())
{
    var products = n.Products;
    IQueryable<Product>? filteredProducts = products?.Where(p => p.UnitsInStock > 10).OrderByDescending(p => p.ProductName);

    var projectedProducts = filteredProducts?.Select(product => new
    {
        product.ProductId,
        product.ProductName,
        product.UnitPrice
    });

    foreach (var p in projectedProducts)
    {
        WriteLine($"{p.ProductId} {p.ProductName} {p.UnitPrice}");
    }

    //Join
    var joined = n.Categories?.Join(products!, c => c.CategoryId, p => p.CategoryId, (c, p) => new { c.CategoryName, p.ProductName, p.UnitsInStock, p.UnitPrice });
    foreach (var p in joined!)
    {
        WriteLine($"{p.CategoryName} {p.ProductName} {p.UnitPrice}");
    }

    WriteLine(n.Products.Max(p => p.UnitPrice));
    WriteLine(n.Products.Average(p => p.UnitPrice));
    WriteLine(n.Products.Min(p => p.UnitPrice));
    WriteLine(n.Products.Max(p => p.UnitPrice));
    WriteLine(n.Products.Sum(p => p.UnitPrice * p.UnitsInStock));

}