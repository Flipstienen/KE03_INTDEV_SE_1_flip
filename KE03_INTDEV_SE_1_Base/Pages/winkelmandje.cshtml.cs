using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Pages.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class winkelmandjeModel : PageModel
    {
        public List<CartItem> products { get; private set; }
        public decimal totalPrice { get; private set; } = 0;
        public void OnGet()
        {
            Console.WriteLine(HttpContext.Session.GetString("cart"));
            
            products = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();

            foreach (var product in products)
            {
                totalPrice += product.price * product.quantity;
            }
            totalPrice.ToString().Replace(",00", "-");

            if (products.Count == 0)
            {
                ViewData["Message"] = "Je winkelmandje is leeg.";
            }
            else
            {
                ViewData["Message"] = null;
            }
        }


        public IActionResult OnPostIncrease(string productName)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.productName == productName);
            if (item != null)
            {
                item.quantity++;
            }
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToPage();
        }

        public IActionResult OnPostDecrease(string productName)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.productName == productName);
            if (item != null)
            {
                item.quantity--;
                if (item.quantity <= 0)
                    cart.Remove(item);
            }

            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(string productName)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.productName == productName);
            if (item != null)
                cart.Remove(item);

            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToPage();
        }
    }

}

