using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Pages.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class Stap1gegevensModel : PageModel
    {
        ICustomerRepository _customerRepository;
        public bool showfeedback { get; set; } = false;
        public List<Customer> customer { get; set; }
        public decimal totalPrice { get; set; } = 0;
        public List<CartItem> cart { get; set; }
        public int huidigeStap { get; set; } = 1;
        public Stap1gegevensModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            customer = new List<Customer>();
        }
        public void OnGet()
        {
            cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            foreach(var item in cart)
            {
                totalPrice += item.price * item.quantity;
            }
            customer = _customerRepository.GetAllCustomers().ToList();    
        }

        public IActionResult OnPostAutentication(string naam, string adres)
        {
            gebruikergegevens gebruiker = new gebruikergegevens(naam, adres);
            HttpContext.Session.SetObjectAsJson("gebruiker", gebruiker);
            return Redirect("stap2bezorgen");
        }
    }
}
