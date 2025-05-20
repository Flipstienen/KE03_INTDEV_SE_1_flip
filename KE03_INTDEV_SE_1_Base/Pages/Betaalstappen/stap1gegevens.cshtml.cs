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

        public Stap1gegevensModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            customer = new List<Customer>();
        }
        public void OnGet()
        {
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
