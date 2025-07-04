﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(string name, string address, bool active)
        {
            Name = name;
            Address = address;
            Active = active;
        }
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public bool Active { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}