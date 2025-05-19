using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Pages.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IProductRepository _productrepository;

        public Product Product { get; set; }

        public DetailsModel(IProductRepository productRepository)
        {
            _productrepository = productRepository;
            Product = new Product();
        }

        public IActionResult OnGet(int id)
        {
            Product = _productrepository.GetProductById(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    
    
        public IActionResult OnPostAddToCart(int productId, int quantity)
        {
            var product = _productrepository.GetProductById(productId);

            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(ci => ci.productId == productId);
            if (cartItem != null)
            {
                cartItem.quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem {productId = product.Id, productName = product.Name, price = product.Price, quantity = quantity });
            }
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToPage("/winkelmandje");
        }
    }
}
