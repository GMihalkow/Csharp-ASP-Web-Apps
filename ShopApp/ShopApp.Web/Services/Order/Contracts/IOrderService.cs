﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Web.Services.Order.Contracts
{
    public interface IOrderService
    {
        Task<ShopApp.Models.Order> Get(string id);

        Task CancelOrder(string orderId);

        Task Checkout(string ordersJson);

        Task SendOrder(string orderId);

        Task CompleteOrder(string orderId);

        IEnumerable<OrderViewModel> GetOrders();
    }
}