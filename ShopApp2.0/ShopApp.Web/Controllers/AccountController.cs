using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Account.Contracts;

namespace ShopApp.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(new LoginInputModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!this.ModelState.IsValid) { return this.View(model); }

            await this._accountService.Login(model);

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

            await this._accountService.Register(model);

            return this.Redirect(this.Url.Action("Index", "Home"));
        }

        [Authorize]
        public ActionResult Logout()
        {
            this._accountService.Logout();

            return this.Redirect(this.Url.Action("Index", "Home"));
        }

        // TODO [GM]: Decide what the my profile functionality will be.
        // [Authorize]
        // public IActionResult MyProfile()
        // {
        //     ProfileViewModel profileModel = this.userService.GetProfileInfo(this.User.Identity.Name);
        //
        //     return this.View(profileModel);
        // }
    }
}