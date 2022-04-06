using Packt.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;

namespace Northwind.WebApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private static ConcurrentDictionary<string, Customer>? customersCache;
        private NorthwindContext db;

        public CustomerRepository(NorthwindContext injectedContext)
        {
            db = injectedContext;
            if (customersCache is null)
            {
                customersCache = new ConcurrentDictionary<string, Customer>(db.Customers.ToDictionary(c => c.CustomerId));
            }
        }
        public async Task<Customer?> CreateAsync(Customer c)
        {
            c.CustomerId = c.CustomerId.ToUpper();
            EntityEntry<Customer> entry = await db.Customers.AddAsync(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                if (customersCache is null) return c;
                return customersCache.AddOrUpdate(c.CustomerId, c, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Customer UpdateCache(string id, Customer c)
        {
            if (customersCache is not null)
            {
                if (customersCache.TryGetValue(id, out Customer? old))
                {
                    if (customersCache.TryUpdate(id, c, old))
                    {
                        return c;
                    }
                }
            }
            return null!;
        }

        public async Task<bool?> DeleteAsync(string id)
        {
            Customer? c = db.Customers.Find(id);
            if (c is null) return null;
            db.Orders.RemoveRange(db.Orders.Where(o => o.CustomerId == c.CustomerId));
            db.Customers.Remove(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                if (customersCache is null) return null;
                return customersCache.TryRemove(id, out c);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Customer>> RetrieveAllAsync()
        {
            return customersCache is null ?
                Task.FromResult(Enumerable.Empty<Customer>()) :
                Task.FromResult(customersCache.Values.OrderBy(c => c.CustomerId).AsEnumerable());
        }

        public Task<Customer?> RetrieveAsync(string id)
        {
            if (customersCache is null) return null!;
            customersCache.TryGetValue(id.ToUpper(), out Customer? c);
            return Task.FromResult(c);
        }

        public async Task<Customer?> UpdateAsync(string id, Customer c)
        {
            id = id.ToUpper();
            c.CustomerId = c.CustomerId.ToUpper();
            db.Customers.Update(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, c);
            }
            return null;
        }
    }
}