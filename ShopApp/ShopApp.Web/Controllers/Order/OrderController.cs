using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Order
{
    public class OrderController : Controller
    {
        [HttpPost]
        public ActionResult Checkout(string products)
        {
            // deserializing the json object to order entities
            ShopApp.Models.Order[] orders = JsonConvert.DeserializeObject<ShopApp.Models.Order[]>(products);

            return this.Redirect("/");
        }
    }
}