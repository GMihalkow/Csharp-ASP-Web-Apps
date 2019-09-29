﻿using ShopApp.Web.Constants;
using ShopApp.Web.Services.Order.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShopApp.Web.Controllers.Order
{
    public class OrderController : BaseController
    {
        public readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        public async Task Checkout(string products)
        {
            await this.orderService.Checkout(products);
        }

        [Authorize]
        public async Task Cancel(string id)
        {
            await this.orderService.CancelOrder(id);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public ActionResult All()
        {
            List<OrderViewModel> orders = this.orderService.GetOrders().ToList();

            return this.View(orders);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task Send(string id)
        {
            await this.orderService.SendOrder(id);
        }

        [Authorize(Roles = RolesConstants.Administrator)]
        public async Task Complete(string id)
        {
            await this.orderService.CompleteOrder(id);
        }
    }
}