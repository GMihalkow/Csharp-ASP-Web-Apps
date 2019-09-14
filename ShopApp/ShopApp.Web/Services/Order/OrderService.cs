﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Web.Constants;
using ShopApp.Web.Services.Account.Contracts;
using ShopApp.Web.Services.Order.Contracts;
using ShopApp.Web.Services.Product.Contracts;

namespace ShopApp.Web.Services.Order
{
    public class OrderService : IOrderService
    {
        private ShopAppDbContext dbContext
        {
            get { return HttpContext.Current.GetOwinContext().Get<ShopAppDbContext>(); }
        }

        public readonly IProductService productService;

        public readonly IAccountService accountService;

        public OrderService(IProductService productService, IAccountService accountService)
        {
            this.accountService = accountService;
            this.productService = productService;
        }

        public async Task Checkout(string ordersJson)
        {
            // getting the logged in user's id
            var userId = this.accountService.GetUser(HttpContext.Current.User.Identity.Name).Id;

            // deserializing the json object to order entities
            ShopApp.Models.Order[] orders = JsonConvert.DeserializeObject<ShopApp.Models.Order[]>(ordersJson);

            if (orders.Any(order => String.IsNullOrEmpty(order.Address)))
            {
                throw new InvalidOperationException("You must provide an address for the Order.");
            }

            // adding the UserId's to the orders
            foreach (var order in orders)
            {
                // generating an ID for every order
                order.Id = Guid.NewGuid().ToString();

                // setting the Status of the order to New
                order.Status = OrderStatus.New;

                // setting the UserId property for every order
                order.UserId = userId;

                this.dbContext.Orders.Add(order);
                this.dbContext.SaveChanges();
            }
        }

        public async Task CancelOrder(string orderId)
        {
            ShopApp.Models.Order order = this.dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null || (order.User.UserName != HttpContext.Current.User.Identity.Name && HttpContext.Current.User.IsInRole(RolesConstants.Administrator)))
            {
                throw new InvalidOperationException("Invalid Order ID.");
            }

            this.dbContext.Orders.Remove(order);
            await this.dbContext.SaveChangesAsync();
        }
    }
}