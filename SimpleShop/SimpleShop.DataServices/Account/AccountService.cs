using DataServices.Common;
using Microsoft.AspNetCore.Identity;
using SimpleShop.Data.Models;
using SimpleShop.DataServices.Interfaces.Account;
using SimpleShop.DataServices.Interfaces.Db;
using SimpleShop.DataServices.Models.Interfaces.Account;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleShop.DataServices.Account
{
    public class AccountService: IAccountService
    {
        private readonly IDbService dbService;
        private readonly UserManager<ShopUser> userManager;
        private readonly SignInManager<ShopUser> signInManager;
        //private readonly IProfileService profileService;

        public AccountService(IDbService dbService, UserManager<ShopUser> userManager, SignInManager<ShopUser> signInManager)
        {
            this.dbService = dbService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            //this.profileService = profileService;
        }

        public void LoginUser(ILoginInputModel model)
        {
            var user = new ShopUser { UserName = model.Username };
            this.OnPostLoginAsync(user, model.Password).GetAwaiter().GetResult();
        }

        public void RegisterUser(IRegisterInputModel model)
        {
            this.OnPostRegister(model, model.Password);
        }

        public void LogoutUser()
        {
            this.signInManager.SignOutAsync().GetAwaiter().GetResult();
        }

        //public bool EmailExists(string email)
        //{
        //    var emailExists = this.dbService.DbContext.Users.Any(u => u.Email == email);

        //    return emailExists;
        //}

        private IdentityResult OnPostRegister(IRegisterInputModel viewModel, string password)
        {
            var model = new ShopUser
            {
                UserName = viewModel.Username,
                Email = viewModel.Email,
                BirthDate = viewModel.BirthDate
            };

            var result = userManager.CreateAsync(model, password).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                if (this.dbService.DbContext.Users.Count() == 1)
                {
                    this.userManager.AddToRoleAsync(model, Role.Owner).GetAwaiter().GetResult();
                }
                else if (this.dbService.DbContext.Users.Count() == 2)
                {
                    this.userManager.AddToRoleAsync(model, Role.Administrator).GetAwaiter().GetResult();
                }
                else
                {
                    this.userManager.AddToRoleAsync(model, Role.User).GetAwaiter().GetResult();
                }

                //if (viewModel.Image != null)
                //{
                //    this.profileService.UploadProfilePicture(viewModel.Image, viewModel.Username);
                //}

                signInManager.SignInAsync(model, isPersistent: false).GetAwaiter().GetResult();
            }


            return result;
        }

        private async Task<SignInResult> OnPostLoginAsync(ShopUser model, string password)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.UserName, password, false, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                throw new System.Exception("User doesn't exist.");
            }

            return result;
        }

        //public bool UserExists(string username)
        //{
        //    var result = this.dbService.DbContext.Users.Any(u => u.UserName == username);

        //    return result;
        //}

        //public async Task<bool> UserWithPasswordExists(string username, string password)
        //{
        //    if (!this.UserExists(username))
        //    {
        //        return false;
        //    }

        //    var User = this.dbService.DbContext.Users.First(u => u.UserName == username);

        //    var result = await this.signInManager.PasswordSignInAsync(User, password, false, false);

        //    return result.Succeeded;
        //}

        public ShopUser GetUser(ClaimsPrincipal principal)
        {
            ShopUser user = this.userManager.GetUserAsync(principal).GetAwaiter().GetResult();

            return user;
        }

        //public string GetNewestUser()
        //{
        //    Models.ForumUser user = this.dbService.DbContext.Users.OrderByDescending(u => u.RegisteredOn).FirstOrDefault();

        //    string username = string.Empty;
        //    if (user != null)
        //    {
        //        username = user.UserName;
        //    }

        //    return username;
        //}

        //public bool UsernameExists(string username)
        //{
        //    var result = this.dbService.DbContext.Users.Any(u => u.UserName == username);

        //    return result;
        //}

        //public Models.ForumUser GetUserById(string id, ModelStateDictionary modelState)
        //{
        //    var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == id);

        //    if (user == null)
        //    {
        //        modelState.AddModelError("error", ErrorConstants.UserNotFoundError);
        //    }

        //    return user;
        //}

        //public Models.ForumUser GetUserByName(string username, ModelStateDictionary modelState)
        //{
        //    var user =
        //        this.dbService
        //        .DbContext
        //        .Users
        //        .Where(u => u.UserName == username)
        //        .FirstOrDefault();

        //    if (user == null)
        //    {
        //        modelState.AddModelError("error", ErrorConstants.UserNotFoundError);
        //    }

        //    return user;
        //}

        //public IEnumerable<string> GetUsernames()
        //{
        //    var usernames = this.GetUsers().Select(u => u.UserName).ToList();

        //    return usernames;
        //}

        //public IEnumerable<Models.ForumUser> GetUsers()
        //{
        //    var users = this.dbService.DbContext.Users.ToList();

        //    return users;
        //}

        //public IEnumerable<string> GetUsernamesWithoutOwner()
        //{
        //    var roleId = this.dbService.DbContext.Roles.FirstOrDefault(r => r.Name == Common.Role.Owner).Id;

        //    var filteredUserIds = this.dbService.DbContext.UserRoles.Where(ur => ur.RoleId != roleId).Select(ur => ur.UserId).ToList();

        //    var usernames = this.GetUsers()
        //        .Where(u => filteredUserIds.Contains(u.Id))
        //        .Select(u => u.UserName)
        //        .ToList();

        //    return usernames;
        //}
    }
}