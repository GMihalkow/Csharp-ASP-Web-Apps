using Microsoft.AspNetCore.Mvc;
using ShopApp.Dal;
using ShopApp.Dal.Repositories.Contracts;

namespace ShopApp.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepository<ProductViewModel, ProductBaseInputModel> _productRepository;

        public HomeController(IRepository<ProductViewModel, ProductBaseInputModel> productRepository)
        {
            _productRepository = productRepository;
        }
    
        public IActionResult Index()
        {
            var products = this._productRepository.GetAll();
            
            return this.View(products);
        }
    }
}