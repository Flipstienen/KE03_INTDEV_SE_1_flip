using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class Filtermodel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public List<string> filters { get; private set; }

        public Filtermodel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            filters = new List<string>();
        }

        public void OnGet()
        {
            filters = _productRepository.GetAllCharasteristics().ToList();
            filters.Add("geen filters");
        }
    }
}