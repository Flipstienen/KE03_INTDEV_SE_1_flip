using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class NotFoundModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public List<Product> Products { get; set; }
        public List<string> Filters { get; set; }

        public string imgSrc { get; set; }
        public string filter { get; set; }
        public NotFoundModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            Products = new List<Product>();
            Filters = new List<string>();
        }
        public IActionResult OnGet(string selectedFilter)
        {
            Filters = _productRepository.GetAllCharasteristics().ToList();
            Filters.Add("geen filters");
            filter = selectedFilter;

            if (selectedFilter == "geen filters")
            {
                Products = _productRepository.GetAllProducts().ToList();
            }

            else
            {
                Products = _productRepository.GetAllProducts().Where(p => p.characteristic == selectedFilter).ToList();
            }

            return Page();
        }
    }
}