using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    public static class NorthwindContextExtensions
    {
        public static IServiceCollection AddNorthwindContext(this IServiceCollection services, string connString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=true;MultipleActiveResultSets=true;Encrypt=false"){
            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(connString));
            return services;
        }
    }
}