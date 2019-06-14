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

            container.RegisterType<IdentityDbContext<ShopUser> , ShopDbContext >();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}