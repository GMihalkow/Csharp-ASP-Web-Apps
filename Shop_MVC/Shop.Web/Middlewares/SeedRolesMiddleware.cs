using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shop.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Shop.Web.Middlewares
{
    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(Microsoft.AspNetCore.Http.HttpContext httpContext, RoleManager<IdentityRole> roleManager)
        {
            RolesSeeder.SeedRoles(roleManager).Wait();

            return this.next(httpContext);
        }
    }
}