using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Shop.Data.Models;
using Shop.Services.Interfaces.Account;
using Shop.ViewModels.Interfaces.Account;
using System;

namespace Shop.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly SignInManager<ShopUser> signInManager;

        public AccountService(UserManager<ShopUser> userManager, SignInManager<ShopUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void RegisterUser(IRegisterInputModel model)
        {
            var entity = new ShopUser(model.Username)
            {
                Age = model.Age,
                RegisteredOn = DateTime.UtcNow
            };

            //var result = userManager.CreateAsync(model, password).GetAwaiter().GetResult();
            //if (result.Succeeded)
            //{
            //    if (this.dbService.DbContext.Users.Count() == 1)
            //    {
            //        this.userManager.AddToRoleAsync(model, Common.Role.Owner).GetAwaiter().GetResult();
            //    }
            //    else if (this.dbService.DbContext.Users.Count() == 2)
            //    {
            //        this.userManager.AddToRoleAsync(model, Common.Role.Administrator).GetAwaiter().GetResult();
            //    }
            //    else
            //    {
            //        this.userManager.AddToRoleAsync(model, Common.Role.User).GetAwaiter().GetResult();
            //    }

            //    if (viewModel.Image != null)
            //    {
            //        this.profileService.UploadProfilePicture(viewModel.Image, viewModel.Username);
            //    }

            //    signInManager.SignInAsync(model, isPersistent: false).GetAwaiter().GetResult();
            //}
        }
    }
}