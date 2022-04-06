using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Mvc.Models;
using Packt.Shared;

namespace Northwind.Mvc.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _db;

        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(ILogger<HomeController> logger, NorthwindContext injectedContext, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _db = injectedContext;
            _httpClientFactory = httpClientFactory;
        }

        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Customer c)
        {
            GenericErrorViewModel<Customer> model = null;

            if (!ModelState.IsValid)
            {
                model = new(
                    entity: c,
                    HasErrors: !ModelState.IsValid,
                    ValidationErrors: ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                );
                return View("Index", model);
            }
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("Northwind.WebApi");
                HttpResponseMessage response = await client.PostAsJsonAsync<Customer>("api/customers", c);
                Customer? newCustomer = await response.Content.ReadFromJsonAsync<Customer>();
                model = new(
                    entity: newCustomer,
                    HasErrors: false,
                    ValidationErrors: Enumerable.Empty<string>());
            }
            catch (Exception ex)
            {
                model = new(
                    entity: c,
                    HasErrors: true,
                    ValidationErrors: new[] { ex.Message }
                );
            }

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            HttpClient client = _httpClientFactory.CreateClient("Northwind.WebApi");
            HttpResponseMessage response = await client.GetAsync("api/customers");
            IEnumerable<Customer>? customers = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            HttpClient client = _httpClientFactory.CreateClient("Northwind.WebApi");
            _logger.LogInformation($"Trying: api/customers?={id}");
            HttpResponseMessage response = await client.DeleteAsync($"api/customers?={id}");
            _logger.LogInformation(response.IsSuccessStatusCode? "Deleted succesfully" : response.ReasonPhrase);
            return RedirectToAction("Search");
        }
    }
}