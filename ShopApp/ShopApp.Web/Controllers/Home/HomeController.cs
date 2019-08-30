using ShopApp.Web.Services.Account.Contracts;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Home
{
	public class HomeController : Controller
	{
		public readonly IAccountService accountService;
		
		public HomeController(IAccountService accountSevice)
		{
			this.accountService = accountSevice;
		}

		public ActionResult Index()
		{
			return this.View();
		}
	}
}