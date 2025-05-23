using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class OrderviewModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPartRepository _partRepository;

        public IList<Order> Orders { get; private set; }
        public IList<Product> Products { get; private set; }
        public IList<Customer> Customers { get; private set; }
        public IList<Part> Parts { get; private set; }
        public List<string> productlist { get; private set; }

        public OrderviewModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            Orders = new List<Order>();
        }
        public void OnGet()
        {
            Orders = _orderRepository.GetAllOrders().ToList();
        }
    }
}
