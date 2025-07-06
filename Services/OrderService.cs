using DAL;
using Entities;
using Services;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderDAL _dal;

        public OrderService()
        {
            _dal = new OrderDAL(); // Uses DBHelper internally
        }

        public Task SaveOrderAsync(OrderModel order)
        {
            _dal.SaveOrder(order);
            return Task.CompletedTask;
        }

        public List<ProductSalesData> GetProductSalesData()
        {
            return _dal.GetProductSalesData();
        }
    }
}
