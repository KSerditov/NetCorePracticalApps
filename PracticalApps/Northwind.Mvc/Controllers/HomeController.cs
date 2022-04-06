using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Mvc.Models;
using Packt.Shared;

namespace Northwind.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly NorthwindContext _db;

    public HomeController(ILogger<HomeController> logger, NorthwindContext injectedContext)
    {
        _logger = logger;
        _db = injectedContext;
    }

    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> Index()
    {
        HomeIndexViewModel model = new()
        {
            VisitorCount = (new Random()).Next(1, 1000),
            Categories = await _db.Categories.ToListAsync(),
            Products = await _db.Products.ToListAsync()
        };
        return View(model);
    }

    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> ProductDetail(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest("Product ID value is required");
        }
        Product? model = await _db.Products.SingleOrDefaultAsync(p => p.ProductId == id);
        if (model == null)
        {
            return NotFound($"Product {id} is not found");
        }
        return View(model);
    }

    [Route("/category/{id}")]
    public async Task<IActionResult> Category(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest("Category ID value is required");
        }
        Category? model = await _db.Categories.SingleOrDefaultAsync(p => p.CategoryId == id);
        if (model == null)
        {
            return NotFound($"Category {id} is not found");
        }
        return View(model);
    }

    [Authorize(Roles = "Administrators")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult ModelBinding()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ModelBinding(Thing thing)
    {
        HomeModelBindingViewModel model = new HomeModelBindingViewModel(
            thing: thing,
            HasErrors: !ModelState.IsValid,
            ValidationErrors: ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
        );
        return View(model);
    }

    [HttpGet]
    public IActionResult ProductsThatCostMoreThan(decimal? price)
    {
        if (!price.HasValue)
        {
            return BadRequest("Price value is required. For example, /home/productsthatcostmorethan?price=50");
        }

        IEnumerable<Product> model = _db.Products.Where(p => p.UnitPrice > price)
            .Include(p => p.Category)
            .Include(p => p.Supplier);

        if (!model.Any())
        {
            return NotFound($"No products with price more than {price} have been found");
        }

        ViewData["MaxPrice"] = price.Value.ToString("C");
        return View(model);
    }

}
