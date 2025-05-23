namespace DataAccessLayer.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public bool Isdelivered { get; set; }
        
        public string DeliveryOption { get; set; }

        public decimal totalPrice { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;


        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    }
}
