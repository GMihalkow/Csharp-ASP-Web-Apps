using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Models;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace Shop.Web
{
    public class TestConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IdentityDbContext<ShopUser>, ShopDbContext>();
            container.RegisterType<UserManager<ShopUser>>();
            container.RegisterType<SignInManager<ShopUser>>();
            container.RegisterType<RoleManager<IdentityRole>>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public void Configure(IApplicationBuilder app)
        {
            //app.UseMiddleware();
        }
    }
}