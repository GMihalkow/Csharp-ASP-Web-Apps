using ShopApp.Dal.Models.User;
using ShopApp.Dal.Services.User.Contracts;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ShopApp.Api.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route(template: "api/User/All")]
        public JsonResult<IEnumerable<UserViewModel>> All()
        {
             var users = this.userService.GetAllUsersViewModels();
            
            return this.Json(users);
        }
    }
}