using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task SaveOrderAsync(OrderModel order);
        List<ProductSalesData> GetProductSalesData();
    }
}
