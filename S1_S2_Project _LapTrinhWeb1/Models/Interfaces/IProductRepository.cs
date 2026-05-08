using S1_S2_Project__LapTrinhWeb1.Models;
namespace S1_S2_Project__LapTrinhWeb1.Models.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetTrendingProducts();
        Product GetProductDetail(int id);
    }
}