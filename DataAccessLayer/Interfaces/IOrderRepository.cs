using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetAllOrders();

        public IEnumerable<Order> GetOrderbyLastOrder();


        public IEnumerable<Order> GetOrdersByOrdered();

        public IEnumerable<Order> GetAllOrdersSortedByDone();

        public void AddOrder(DateTime date, int customerId, bool isDelivered);
        public Order? GetOrderById(int id);

        public void AddOrder(Order order);

        public void UpdateOrder(Order order);

        public void DeleteOrder(Order order);
    }
}
