using Microsoft.AspNetCore.Mvc;
using S1_S2_Project__LapTrinhWeb1.Models;
using S1_S2_Project__LapTrinhWeb1.Models.Interfaces;

namespace S1_S2_Project__LapTrinhWeb1.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepository;

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View(productRepository.GetTrendingProducts());
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}