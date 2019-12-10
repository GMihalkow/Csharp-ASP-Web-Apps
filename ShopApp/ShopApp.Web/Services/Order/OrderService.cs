using Newtonsoft.Json;
using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Web.Constants;
using ShopApp.Web.Models;
using ShopApp.Web.Repositories.Contracts;
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
        public readonly IAccountService accountService;
        private readonly ShopAppDbContext dbContext;
        private readonly IProductService productService;
        private readonly IRepository<ProductViewModel, ProductBaseInputModel> productRepository;

        public OrderService(IAccountService accountService, ShopAppDbContext dbContext, IProductService productService, IRepository<ProductViewModel, ProductBaseInputModel> productRepository)
        {
            this.accountService = accountService;
            this.dbContext = dbContext;
            this.productService = productService;
            this.productRepository = productRepository;
        }

        public async Task<string> Checkout(string ordersJson)
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

            var outputMessages = new List<string>();

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
                ProductViewModel product = this.productRepository.Get(order.ProductId);
                if (product == null)
                {
                    outputMessages.Add($"{{ \"productId\":  \"{product.Id}\", \"type\": \"error\", \"message\": \"Invalid product Id {product.Id}.\" }}");

                    continue;
                }
                else if (product.StockCount - order.Quantity < 0)
                {
                    outputMessages.Add($"{{ \"productId\":  \"{product.Id}\", \"type\": \"error\", \"message\": \"Insufficient quantity of product {product.Name}.\" }}");

                    continue;
                }

                await this.productService.DecrementProductStockCount(order.ProductId, order.Quantity);

                if (product.StockCount - order.Quantity == 0)
                {
                    outputMessages.Add($"{{ \"productId\": \"{product.Id}\", \"type\": \"outOfStock\", \"message\": \"out of stock\" }}");
                }

                order.ProductId = product.Id;

                this.dbContext.Orders.Add(order);
                await this.dbContext.SaveChangesAsync();
            }

            return JsonConvert.SerializeObject(outputMessages);
        }

        public async Task CancelOrder(string orderId)
        {
            ShopApp.Models.Order order = this.dbContext.Orders.Include(o => o.User).FirstOrDefault(o => o.Id == orderId);

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
                    Status = order.Status.ToString(),
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
            ShopApp.Models.Order order = await this.Get(orderId);

            order.Status = OrderStatus.Sent;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task CompleteOrder(string orderId)
        {
            ShopApp.Models.Order order = await this.Get(orderId);

            order.Status = OrderStatus.Completed;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ShopApp.Models.Order> Get(string id)
        {
            // TODO [GM]: Avoid using Task.Run when having no asynchronous work to do
            return await Task.Run(() =>
            {
                ShopApp.Models.Order order = this.dbContext.Orders.FirstOrDefault(o => o.Id == id);

                if (order == null) { throw new InvalidOperationException("Invalid order ID."); }

                return order;
            });
        }
    }
}