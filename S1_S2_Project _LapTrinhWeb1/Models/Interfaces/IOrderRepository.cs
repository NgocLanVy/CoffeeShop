using S1_S2_Project__LapTrinhWeb1.Models;

namespace S1_S2_Project__LapTrinhWeb1.Models.Interfaces
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order);
    }
}