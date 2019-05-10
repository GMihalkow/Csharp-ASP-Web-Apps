using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using SimpleShop.Web.Utilities;
using System.Threading.Tasks;

namespace SimpleShop.Web.Middlewares
{
    public class RolesSeederMiddleware
    {
        private readonly RequestDelegate next;

        public RolesSeederMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext httpContext, RoleManager<IdentityRole> roleManager)
        {
            Seeder.SeedRoles(roleManager).Wait();

            return this.next(httpContext);
        }
    }
}