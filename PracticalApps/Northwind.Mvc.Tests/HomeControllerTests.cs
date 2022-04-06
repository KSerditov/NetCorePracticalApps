using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;
using Moq;
using Xunit;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using Northwind.Mvc.Controllers;
using Microsoft.Extensions.Logging;


namespace Northwind.Mvc.Tests;

public class HomeControllerTests
{
    [Fact]
    public async Task Index_ReturnsAViewResult_WithAListProductsCategories()
    {
        var categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "TestCategory1", Description = "TestDescription1"},
                new Category { CategoryId = 2, CategoryName = "TestCategory2", Description = "TestDescription2" },
                new Category { CategoryId = 3, CategoryName = "TestCategory3", Description = "TestDescription3" },
            }.AsQueryable();

        var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Product1", CategoryId = 1, QuantityPerUnit = "2", UnitPrice = 150.34m, UnitsInStock = 100, Discontinued = false  },
                new Product { ProductId = 2, ProductName = "Product2", CategoryId = 1, QuantityPerUnit = "1", UnitPrice = 142m, UnitsInStock = 50, Discontinued = false },
                new Product { ProductId = 3, ProductName = "Product3", CategoryId = 2, QuantityPerUnit = "1", UnitPrice = 0.4m, UnitsInStock = 1, Discontinued = false },
            }.AsQueryable();

        var mockSetCategories = new Mock<DbSet<Category>>();
        mockSetCategories.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(categories.Provider);
        mockSetCategories.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
        mockSetCategories.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
        mockSetCategories.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

        var mockSetProducts = new Mock<DbSet<Product>>();
        mockSetProducts.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
        mockSetProducts.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
        mockSetProducts.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
        mockSetProducts.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

        var mockContext = new Mock<NorthwindContext>();
        mockContext.Setup(c => c.Categories).Returns(mockSetCategories.Object);
        mockContext.Setup(c => c.Products).Returns(mockSetProducts.Object);

        var mockLogger = new Mock<ILogger<HomeController>>();

        var controller = new HomeController(mockLogger.Object, mockContext.Object);

        var result = await controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<HomeIndexViewModel>>(viewResult.ViewData.Model);
        Assert.Equal(1, model.Count());
    }
}