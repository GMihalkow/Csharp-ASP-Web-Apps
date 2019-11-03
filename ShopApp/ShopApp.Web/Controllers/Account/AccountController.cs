using ShopApp.Web.Models;
using ShopApp.Web.Services.Account.Contracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Account
{
    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
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

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

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

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

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
        public async Task<ActionResult> MyProfile()
        {
            ProfileViewModel profileModel = await this.accountService.GetProfileInfo(this.User.Identity.Name);

            return this.View(profileModel);
        }
    }
}