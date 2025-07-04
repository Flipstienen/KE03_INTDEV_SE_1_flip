﻿namespace DataAccessLayer.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string characteristic { get; set; }

        public byte[] Image { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public ICollection<Part> Parts { get; } = new List<Part>();

    }
}
