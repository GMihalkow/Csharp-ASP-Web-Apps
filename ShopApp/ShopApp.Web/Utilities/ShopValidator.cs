using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Web.Services.Account.Contracts;

namespace ShopApp.Web.Utilities
{
    public class ShopValidator : UserValidator<ShopUser>
    {
        private ShopAppDbContext dbContext
        {
            get { return HttpContext.Current.GetOwinContext().Get<ShopAppDbContext>(); }
        }

        public ShopValidator(UserManager<ShopUser, string> manager) : base(manager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(ShopUser item)
        {
            await base.ValidateAsync(item);

            if (this.dbContext.Users.Any(user => user.UserName == item.UserName))
            {
                throw new InvalidOperationException("An account with this username already exists.");
            }
            else if (this.dbContext.Users.Any(user => user.Email == item.Email))
            {
                throw new InvalidOperationException("An account with this email already exists.");
            }
            
            return IdentityResult.Success;
        }
    }
}