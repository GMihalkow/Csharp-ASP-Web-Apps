using Newtonsoft.Json;
using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Services.Account.Contracts;
using ShopApp.Web.Services.Order.Contracts;
using ShopApp.Web.Services.Product.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShopApp.Web.Services.Order
{
    public class OrderService : IOrderService
    {
        public readonly IProductService productService;

        public readonly IAccountService accountService;
        private readonly ShopAppDbContext dbContext;

        public OrderService(IProductService productService, IAccountService accountService, ShopAppDbContext dbContext)
        {
            this.accountService = accountService;
            this.dbContext = dbContext;
            this.productService = productService;
        }

        public async Task Checkout(string ordersJson)
        {
            // getting the logged in user's id
            var user = await this.accountService.GetUser(HttpContext.Current.User.Identity.Name);

            // deserializing the json object to order entities
            ShopApp.Models.Order[] orders = JsonConvert.DeserializeObject<ShopApp.Models.Order[]>(ordersJson);

            // filtering the not valid quantity values
            orders = orders.Where(o => o.Quantity > 0).ToArray();

            if (orders.Any(order => string.IsNullOrEmpty(order.Address)))
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
                order.UserId = user.Id;

                // setting the ordered on to utc now
                order.OrderedOn = DateTime.UtcNow;

                // making sure that we have the correct product with the correct price
                ProductViewModel product = this.productService.Get(order.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException("Invalid product id.");
                }

                order.ProductId = product.Id;
                
                this.dbContext.Orders.Add(order);
                await this.dbContext.SaveChangesAsync();
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

        public IEnumerable<OrderViewModel> GetOrders()
        {
            IEnumerable<OrderViewModel> orders =
                this.dbContext.Orders
                .Select(order => new OrderViewModel
                {
                    Id = order.Id,
                    Address = order.Address,
                    Description = order.Description,
                    Quantity = order.Quantity,
                    OrderedOn = order.OrderedOn,
                    Status = order.Status,
                    User = order.User.UserName,
                    UserId = order.UserId,
                    Product = new Models.ProductViewModel
                    {
                        Id = order.Product.Id,
                        Description = order.Product.Description,
                        AddedOn = order.Product.AddedOn,
                        CategoryId = order.Product.CategoryId,
                        CategoryName = order.Product.Category.Name,
                        CoverUrl = order.Product.CoverUrl,
                        Name = order.Product.Name,
                        Price = order.Product.Price
                    },
                });

            return orders;
        }

        public async Task SendOrder(string orderId)
        {
            ShopApp.Models.Order order = await this.GetOrder(orderId);

            order.Status = OrderStatus.Sent;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task CompleteOrder(string orderId)
        {
            ShopApp.Models.Order order = await this.GetOrder(orderId);

            order.Status = OrderStatus.Completed;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ShopApp.Models.Order> GetOrder(string id)
        {
            ShopApp.Models.Order order = this.dbContext.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                throw new InvalidOperationException("Invalid order ID.");
            }

            return order;
        }
    }
}