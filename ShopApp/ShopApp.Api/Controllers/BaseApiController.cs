using System.Web.Http;
using System.Web.Mvc;

namespace ShopApp.Api.Controllers
{
    [RequireHttps]
    public abstract class BaseApiController : ApiController { }
}