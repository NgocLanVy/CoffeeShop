using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using S1_S2_Project__LapTrinhWeb1.Models;
using S1_S2_Project__LapTrinhWeb1.Models.Interfaces;

namespace S1_S2_Project__LapTrinhWeb1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public OrdersController(
            IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository)
        {
            this.orderRepository = orderRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            orderRepository.PlaceOrder(order);

            shoppingCartRepository.ClearCart();

            HttpContext.Session.SetInt32("CartCount", 0);

            return RedirectToAction("CheckoutComplete");
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}