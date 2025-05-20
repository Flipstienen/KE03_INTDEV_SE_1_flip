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

        public IActionResult OnPostBestel(string value)
        {
            if (value == "thuis")
            {
                HttpContext.Session.SetString("levering", "thuis");
            }
            else
            {
                HttpContext.Session.SetString("levering", "afhaalpunt");
            }

            return Redirect("stap3betalen");
        }
    }
}
