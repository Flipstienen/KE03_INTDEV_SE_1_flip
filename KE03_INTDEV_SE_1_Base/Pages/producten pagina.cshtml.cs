using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly MatrixIncDbContext _context;

        public DetailsModel(MatrixIncDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products.FindAsync(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }