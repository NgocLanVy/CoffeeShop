using S1_S2_Project__LapTrinhWeb1.Data;
using S1_S2_Project__LapTrinhWeb1.Models.Interfaces;

namespace S1_S2_Project__LapTrinhWeb1.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        private CoffeeshopDbContext dbContext;

        public ProductRepository(CoffeeshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return dbContext.Products;
        }
        public Product GetProductDetail(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<Product> GetTrendingProducts()
        {
            return dbContext.Products.Where(p => p.IsTrendingProduct);
        }
    }
}