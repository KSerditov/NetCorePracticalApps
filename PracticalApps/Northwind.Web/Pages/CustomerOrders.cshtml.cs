using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Packt.Shared;

namespace Northwind.Web.Pages;

public class CustomerOrdersModel : PageModel
{
    private NorthwindContext db;

    public Customer? Customer { get; set; }

    public CustomerOrdersModel(NorthwindContext injectedContext)
    {
        db = injectedContext;
    }

    public void OnGet(string id)
    {
        Customer = db.Customers.Where(c => c.CustomerId.Equals(id)).Include(c => c.Orders).FirstOrDefault();
    }

}