using DataAccessLayer.Models;

namespace DataAccessLayer.Pages
{
    public class CartItem
    {
        public int productId { get; set; }
        public string productName { get; set; }

        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}
