using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Pages.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages.Betaalstappen
{
    public class stap3betalenModel : PageModel
    {
        public IOrderRepository _orderRepository { get; set; }
        public ICustomerRepository _customerRepository { get; set; }
        public IProductRepository _productRepository { get; set; }
        private decimal totale_prijs_decimal { get; set; } = 0;
        public string totale_prijs { get; set; }
        public decimal levering_prijs { get; set; } = 0;
        public string levering { get; set; }
        public List<CartItem> cart { get; set; } = new List<CartItem>();
        public string naam { get; set; }
        public gebruikergegevens gebruiker { get; set; } = new gebruikergegevens();
        public int huidigeStap { get; set; } = 3;

        public bool isDelivered { get; set; }

        public stap3betalenModel(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }
        public void OnGet()
        {
            cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            foreach (var item in cart)
            {
                totale_prijs_decimal += item.price * item.quantity;
            }

            levering = HttpContext.Session.GetString("levering");

            if (levering == "instant")
            {
                levering_prijs = 5;
                totale_prijs_decimal += 5;
            }

            totale_prijs = totale_prijs_decimal.ToString("N2", new System.Globalization.CultureInfo("nl-NL")).Replace(",00", ",-");
        }

        public IActionResult OnPostBetaald()
        {
            cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            foreach (var item in cart)
            {
                totale_prijs_decimal += item.price * item.quantity;
            }

            string levering = HttpContext.Session.GetString("levering");
            if (levering == "instant")
            {
                totale_prijs_decimal += 5;
                isDelivered = true;
            }
            else
            {
                isDelivered = false;
            }
            gebruiker = HttpContext.Session.GetObjectFromJson<gebruikergegevens>("gebruiker");

            naam = gebruiker.naam;
            if (_customerRepository.GetCustomerByName(gebruiker.naam) == null)
            {
                _customerRepository.AddCustomer(new Customer(gebruiker.naam, gebruiker.adres, true));
            }


            var customer = _customerRepository.GetCustomerByName(naam);

            var nieuwe_order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = customer.Id,
                Isdelivered = isDelivered,
                DeliveryOption = levering,
                totalPrice = totale_prijs_decimal,
            };

            cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            foreach (var item in cart)
            {
                var product = _productRepository.GetProductById(item.productId);
                if (product != null)
                {
                    nieuwe_order.OrderProducts.Add(new OrderProduct
                    {
                        ProductId = product.Id,
                        Order = nieuwe_order,
                        Amount = item.quantity,
                    });
                }
            }

            _orderRepository.AddOrder(nieuwe_order);
            HttpContext.Session.Clear();
            Console.WriteLine("Bestelling is geplaatst");
            return Redirect("~/index");
        }
    }
}
