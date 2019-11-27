using System.Web.Mvc;

namespace ShopApp.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName { get { return "Admin"; } }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "AdminAreaDefault",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional },
                constraints: new { controller = "Administration" },
                namespaces: new[] { "ShopApp.Web.Areas.Admin.Controllers" }
            );
        }
    }
}