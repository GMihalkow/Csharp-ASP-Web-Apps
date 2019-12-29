using ShopApp.Dal;
using ShopApp.Dal.Services.User.Contracts;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Account.Contracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            this.accountService = accountService;
            this.userService = userService;
        }

        public ActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(new LoginInputModel());
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginInputModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!this.ModelState.IsValid) { return this.View(model); }

            await this.accountService.Login(model);

            return this.Redirect("/");
        }

        public ActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(new RegisterInputModel());
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterInputModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!this.ModelState.IsValid) { return this.View(model); }

            await this.accountService.Register(model);

            return this.Redirect(this.Url.Action("Index", "Home"));
        }

        [Authorize]
        public ActionResult Logout()
        {
            this.accountService.Logout();

            return this.Redirect(this.Url.Action("Index", "Home"));
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            ProfileViewModel profileModel = this.userService.GetProfileInfo(this.User.Identity.Name);

            return this.View(profileModel);
        }
    }
}