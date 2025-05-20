using KE03_INTDEV_SE_1_Base.Pages.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using System.Security.Cryptography.X509Certificates;
using DataAccessLayer.Interfaces;
using System.Runtime.Loader;

namespace KE03_INTDEV_SE_1_Base.Pages.Betaalstappen
{
    public class stap3betalenModel : PageModel
    {
        public IOrderRepository _orderRepository { get; set; }
        public ICustomerRepository _customerRepository { get; set; }
        public IProductRepository _productRepository { get; set; }
        private decimal totale_prijs_decimal { get; set; } = 0;
        public string totale_prijs { get; set; } = "0";
        public string naam { get; set; }
        public List<gebruikergegevens> gebruiker { get; set; } = new List<gebruikergegevens>();

        public stap3betalenModel(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }
        public void OnGet()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            foreach(var item in cart)
            {
                totale_prijs += item.price * item.quantity;
            }

            string levering = HttpContext.Session.GetString("levering");
            if (levering == "thuis")
            {
                totale_prijs_decimal += 5;
            }
            else
            {
                totale_prijs_decimal += 0;
            }
            totale_prijs.ToString().Replace(",0", "-");
        }

        public IActionResult OnPostBetaald()
        {
            gebruiker = HttpContext.Session.GetObjectFromJson<List<gebruikergegevens>>("gebruiker");

            foreach (var item in gebruiker)
            {
                naam = item.naam;
                if (_customerRepository.GetCustomerByName(item.naam) == null)
                {
                    _customerRepository.AddCustomer(new Customer(item.naam, item.adres, true));
                }
            }
            _orderRepository.AddOrder(DateTime.Now, _customerRepository.GetCustomerByName(naam).Id);

            var order = _orderRepository.GetOrderLastOrder();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            foreach (var item in cart)
            {
                var product = _productRepository.GetProductById(item.productId);
                if (product != null)
                {
                    order.Products
                }
            }

            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
