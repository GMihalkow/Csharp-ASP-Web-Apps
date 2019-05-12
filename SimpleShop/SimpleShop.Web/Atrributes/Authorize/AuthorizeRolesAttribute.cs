using Microsoft.AspNetCore.Authorization;

namespace SimpleShop.Web.Attributes.Authorize
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) 
        {
            Roles = string.Join(", ", roles);
        }
    }
}