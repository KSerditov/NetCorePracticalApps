using Microsoft.AspNetCore.Mvc;
using Packt.Shared;
using Northwind.WebApi.Repositories;

namespace Northwind.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repo;

        public CustomersController(ICustomerRepository injectedRepository)
        {
            _repo = injectedRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public async Task<IEnumerable<Customer>>? GetCustomers(string? country)
        {
            if (String.IsNullOrWhiteSpace(country))
            {
                return await _repo.RetrieveAllAsync();
            }
            else
            {
                return (await _repo.RetrieveAllAsync()).Where(c => c.Country == country);
            }

        }

        [HttpGet("{id}", Name = nameof(GetCustomer))]
        [ProducesResponseType(200, Type = typeof(Customer))]
        [ProducesResponseType(404)]
        public async Task<IActionResult>? GetCustomer(string id)
        {
            Customer? c = await _repo.RetrieveAsync(id);
            if (c is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(c);
            }
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type = typeof(Customer))]
        public async Task<IActionResult>? Create([FromBody] Customer c)
        {
            if (c is null)
            {
                return BadRequest();
            }
            Customer? newCustomer = await _repo.CreateAsync(c);
            if (newCustomer is null)
            {
                return BadRequest("Repository failed to create new customer");
            }
            else
            {
                return CreatedAtRoute(
                    routeName: nameof(GetCustomer),
                    routeValues: new { id = newCustomer.CustomerId },
                    value: newCustomer
                    );
            }
        }

        [HttpPut]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult>? Update(string id, [FromBody] Customer c)
        {
            id = id.ToUpper();
            c.CustomerId = c.CustomerId.ToUpper();
            if (String.IsNullOrWhiteSpace(id) || !id.Equals(c.CustomerId))
            {
                return BadRequest();
            }
            Customer? target = await _repo.RetrieveAsync(id);
            if (target is null)
            {
                return NotFound();
            }
            Customer? modifiedCustomer = await _repo.UpdateAsync(id, c);
            if (modifiedCustomer is null)
            {
                return BadRequest("Repository is not able to update Customer");
            }
            else
            {
                return new NoContentResult();
            }
        }

        [HttpDelete]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult>? Delete(string id)
        {
            if (id == "bad")
            {
                ProblemDetails problemDetails = new()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://localhost:5001/customers/failed-to-delete", //how to make this dynamic?
                    Title = $"Customer {id} is found but failed to delete",
                    Detail = "More details like Company Name, Country and so on.",
                    Instance = HttpContext.Request.Path
                };
                return BadRequest(problemDetails);
            }

            if (String.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            Customer? existing = await _repo.RetrieveAsync(id);
            if (existing is null)
            {
                return NotFound();
            }
            
            bool? deleted = await _repo.DeleteAsync(id);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();
            }
            else
            {
                return BadRequest("Repository is not able to delete");
            }
        }
    }
}