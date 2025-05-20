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

        public Stap2bezorgenModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            customer = new List<Customer>();
        }
        public void OnGet()
        {
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
