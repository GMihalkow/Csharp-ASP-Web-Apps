using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopApp.Web.Models
{
    public class ProductEditModel : ProductBaseInputModel
    {
        public string Id { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}