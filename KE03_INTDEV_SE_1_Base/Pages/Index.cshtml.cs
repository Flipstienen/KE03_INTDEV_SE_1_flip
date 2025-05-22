using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderProductsRepository _orderProductsRepository;


        public IList<Customer> Customers { get; private set; }

        public IList<Product> MostSoldProducts { get; private set; }
        public IndexModel(ILogger<IndexModel> logger, ICustomerRepository customerRepository, IOrderProductsRepository orderProductsRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _orderProductsRepository = orderProductsRepository;
            Customers = new List<Customer>();
            MostSoldProducts = new List<Product>();
        }

        public void OnGet()
        {
            HttpContext.Session.SetString("test", "hello");
            var value = HttpContext.Session.GetString("test");
            Console.WriteLine("SESSION VALUE: " + value); // Should print "hello"

            Customers = _customerRepository.GetAllCustomers().ToList();
            _logger.LogInformation($"getting all {Customers.Count} customers");
            MostSoldProducts = _orderProductsRepository.GetAllMostSoldProducts().ToList();
        }
    }
}
