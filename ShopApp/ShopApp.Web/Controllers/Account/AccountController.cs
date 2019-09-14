﻿using ShopApp.Web.Models;
using System.Web.Mvc;
using ShopApp.Web.Services.Account.Contracts;
using System.Threading.Tasks;

namespace ShopApp.Web.Controllers.Account
{
    public class AccountController : Controller
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

            return this.Redirect(Url.Action("Index", "Home"));
        }

        [Authorize]
        public ActionResult Logout()
        {
            this.accountService.Logout();

            return this.Redirect(Url.Action("Index", "Home"));
        }


        [Authorize]
        public ActionResult MyProfile()
        {
            ProfileViewModel profileModel = this.accountService.GetProfileInfo(this.User.Identity.Name);

            return this.View(profileModel);
        }
    }
}