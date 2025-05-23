using System.Reflection.Metadata;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Pages.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class Stap2bezorgenModel : PageModel
    {
        ICustomerRepository _customerRepository;

        public List<Customer> customer { get; set; }
        public List<CartItem> cart { get; set; }
        public decimal totalPrice { get; set; } = 0;
        public int huidigeStap { get; set; } = 2;

        public Stap2bezorgenModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            customer = new List<Customer>();
        }
        public void OnGet()
        {
            cart= HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            foreach (var item in cart)
            {
                totalPrice += item.price * item.quantity;
            }
            customer = _customerRepository.GetAllCustomers().ToList();
        }

        public IActionResult OnPostBestel(string bezorgoptie)
        {


            if (string.IsNullOrEmpty(bezorgoptie))
            {
                ModelState.AddModelError(string.Empty, "Selecteer een bezorgmethode.");
                return Page();
            }
            HttpContext.Session.SetString("levering", bezorgoptie);
            return Redirect("stap3betalen");
        }
    }
}
