using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Account.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using ShopApp.Web.Services.Role.Contracts;

namespace ShopApp.Web.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly ShopAppDbContext _dbContext;
        private readonly UserManager<ShopUser> _userManager;
        private readonly SignInManager<ShopUser> _signInManager;
        private readonly IRoleService _roleService;

        public AccountService(ShopAppDbContext dbContext, UserManager<ShopUser> userManager,
            SignInManager<ShopUser> signInManager, IRoleService roleService)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleService = roleService;
        }

        public async Task Register(RegisterInputModel model)
        {
            await this._roleService.SeedRoles();

            var user = new ShopUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                RegisteredOn = DateTime.UtcNow,
                Email = model.Email,
                BirthDate = model.BirthDate
            };

            if (this._userManager.Users.Any(u => u.UserName == user.UserName))
            {
                throw new InvalidOperationException("Username is already taken.");
            }
            else if (this._userManager.Users.Any(u => u.Email == user.Email))
            {
                throw new InvalidOperationException("Email already exists.");
            }

            IdentityResult result = await this._userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to register user.");
            }

            if (this._dbContext.Users.Count() == 1)
            {
                await this._userManager.AddToRoleAsync(user, RolesConstants.Administrator);
            }
            else if (this._dbContext.Users.Count() > 1)
            {
                await this._userManager.AddToRoleAsync(user, RolesConstants.User);
            }

            await this._signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string returnUrl)
        {
            return this._signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
        }

        public async Task ExternalLogin()
        {
            var info = await this._signInManager.GetExternalLoginInfoAsync();

            var email = info.Principal.Claims.First(x => x.Type.EndsWith("/emailaddress")).Value;

            var user = new ShopUser
            {
                UserName = email,
                Email = email
            };

            if (!(this._dbContext.Users.Any(x => x.UserName == user.UserName)))
            {
                var createUserResult = await _userManager.CreateAsync(user);

                if (createUserResult.Succeeded)
                {
                    if (this._dbContext.Users.Any())
                    {
                        await this._userManager.AddToRoleAsync(user, RolesConstants.User);
                    }
                    else
                    {
                        await this._userManager.AddToRoleAsync(user, RolesConstants.Administrator);
                    }

                    createUserResult = await _userManager.AddLoginAsync(user, info);

                    if (createUserResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid email address.");
                }
            }

            var result =
                await this._signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);
        }

        public async Task Login(LoginInputModel model)
        {
            var signInResult =
                await this._signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);

            if (signInResult != SignInResult.Success)
            {
                throw new InvalidOperationException("Incorrect username or password.");
            }
        }

        public async Task Logout() => await this._signInManager.SignOutAsync();
    }
}