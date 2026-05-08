using Microsoft.AspNetCore.Mvc;
using S1_S2_Project__LapTrinhWeb1.Models.Interfaces;

namespace S1_S2_Project__LapTrinhWeb1.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IActionResult Shop()
        {
            return View(productRepository.GetAllProducts());
        }
    }
}