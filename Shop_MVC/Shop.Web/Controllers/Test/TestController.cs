using Shop.Data;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers.Test
{
    public class TestController : Controller
    {
        private readonly ShopDbContext dbContext;
        public TestController(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ActionResult Route()
        {
            return this.View();
        }
    }
}