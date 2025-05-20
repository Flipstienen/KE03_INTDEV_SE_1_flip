using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MatrixIncDbContext _context;

        public OrderRepository(MatrixIncDbContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddOrder(DateTime date, int customerId)
        {
            Order order = new Order
            {
                OrderDate = date,
                CustomerId = customerId
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetOrderbyLastOrder()
        {
            return _context.Orders.OrderByDescending(o => o.OrderDate).Take(1);
        }
 
        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Customer).Include(o => o.OrderProducts).ThenInclude(op => op.Product).ThenInclude(p => p.Parts);
        }

        public IEnumerable<Order> GetAllOrdersSortedByDone()
        {
            return _context.Orders.Include(o => o.Customer).Include(o => o.OrderProducts).ThenInclude(op => op.Product).ThenInclude(p => p.Parts).OrderByDescending(o => o.Isdelivered);
        }

        public Order? GetOrderById(int id)
        {
            return _context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == id);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}
