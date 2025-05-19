using DataAccessLayer.Models;

namespace DataAccessLayer.Models
{
    public class CartItem
    {
        public int productId { get; set; }
        public string productName { get; set; }

        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}
