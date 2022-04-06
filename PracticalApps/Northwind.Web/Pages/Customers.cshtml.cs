using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using Packt.Shared;

namespace Northwind.Web.Pages;

public class CustomersModel : PageModel
{
    private NorthwindContext db;

    [BindProperty]
    public Customer? Customer { get; set; }

    public CustomersModel(NorthwindContext injectedContext)
    {
        db = injectedContext;
    }

    public IEnumerable<Customer>? Customers { get; set; }

    public IEnumerable<IGrouping<string?, Customer>>? CustomersByCountry { get; set; }

    public void OnGet()
    {
        //Customers = db.Customers.OrderBy(c => c.Country).AsEnumerable();
        CustomersByCountry = db.Customers.AsEnumerable().GroupBy(c => c.Country);
    }

}