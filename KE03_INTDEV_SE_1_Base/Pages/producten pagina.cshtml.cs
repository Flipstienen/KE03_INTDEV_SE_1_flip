using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using KE03_INTDEV_SE_1_Base.Pages.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IProductRepository _productrepository;

        public Product Product { get; set; }
        public string imgSrc { get; set; }

        public bool showFeedback { get; set; } = false;
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
            imgSrc = $"data:image/jpeg;base64,{Convert.ToBase64String(Product.Image)}";
            return Page();
        }
    
    
        public IActionResult OnPostAddToCart(int productId, int quantity)
        {
            Product = _productrepository.GetProductById(productId);
            showFeedback = false;
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
            imgSrc = $"data:image/jpeg;base64,{Convert.ToBase64String(product.Image)}";
            showFeedback = true;
            return Page();
        }
    }
}
