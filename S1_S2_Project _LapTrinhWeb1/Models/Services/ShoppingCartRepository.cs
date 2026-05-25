using Microsoft.EntityFrameworkCore;
using S1_S2_Project__LapTrinhWeb1.Data;
using S1_S2_Project__LapTrinhWeb1.Models.Interfaces;

namespace S1_S2_Project__LapTrinhWeb1.Models.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private CoffeeshopDbContext dbContext;

        public ShoppingCartRepository(CoffeeshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
        public string? ShoppingCartId { get; set; }

        //tạo hoặc lấy Cart từ Session theo GUID
        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
                                        ?.HttpContext?.Session;

            CoffeeshopDbContext context = services.GetService<CoffeeshopDbContext>()
                ?? throw new Exception("Error initializing CoffeeshopDbContext");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);

            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }

        //thêm sp vào giỏ, nếu đã có thì tăng slg
        public void AddToCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems
                .FirstOrDefault(s => s.Product!.Id == product.Id
                                  && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1,
                };
                dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }

            dbContext.SaveChanges();
        }

        //xóa 1 sp, nếu qty > 1 thì giảm slg
        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = dbContext.ShoppingCartItems
                .FirstOrDefault(s => s.Product!.Id == product.Id
                                  && s.ShoppingCartId == ShoppingCartId);

            var quantity = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                    quantity = shoppingCartItem.Qty;
                }
                else
                {
                    dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            dbContext.SaveChanges();
            return quantity;
        }

        //lấy all items trong giỏ của user hiện tại
        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            return ShoppingCartItems ??= dbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product)
                .ToList();
        }

        //xóa all giỏ hàng
        public void ClearCart()
        {
            var cartItems = dbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId);
            dbContext.ShoppingCartItems.RemoveRange(cartItems);
            dbContext.SaveChanges();
        }

        //tính tổng tiền
        public decimal GetShoppingCartTotal()
        {
            var totalCost = dbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(s => s.Product!.Price * s.Qty)
                .Sum();
            return totalCost;
        }
    }
}