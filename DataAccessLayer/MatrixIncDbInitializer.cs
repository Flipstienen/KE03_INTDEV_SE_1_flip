﻿using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // Look for any customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            // TODO: Hier moet ik nog wat namen verzinnen die betrekking hebben op de matrix.
            // - Denk aan de m3 boutjes, moertjes en ringetjes.
            // - Denk aan namen van schepen
            // - Denk aan namen van vliegtuigen            
            var customers = new Customer[]
            {
                new Customer { Name = "Neo", Address = "123 Elm St" , Active=true},
                new Customer { Name = "Morpheus", Address = "456 Oak St", Active = true },
                new Customer { Name = "Trinity", Address = "789 Pine St", Active = true }
            };
            context.Customers.AddRange(customers);

            var orders = new Order[]
            {
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01"),Isdelivered=true, DeliveryOption="instant", totalPrice=11001.00m},
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01"),Isdelivered=false, DeliveryOption = "afhalen", totalPrice = 129.99m},
                new Order { Customer = customers[1], OrderDate = DateTime.Parse("2021-02-01"),Isdelivered=false, DeliveryOption = "afhalen", totalPrice = 30000.00m},
                new Order { Customer = customers[2], OrderDate = DateTime.Parse("2021-03-01"),Isdelivered=true, DeliveryOption = "instant", totalPrice= 2002.00m}
            };
            context.Orders.AddRange(orders);

            var products = new Product[]
            {

                new Product { Name = "Nebuchadnezzar", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m, characteristic = "Voertuigen", Image = System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), @"..\", "DataAccessLayer", "Images_For_Database", "Hoverbike.jpg")) },
                new Product { Name = "Jack-in Chair", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m, characteristic="Zetels", Image = System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), @"..\", "DataAccessLayer", "Images_For_Database", "jack-in chair.jpeg")) },
                new Product { Name = "EMP (Electro-Magnetic Pulse) Device", Description = "Wapentuig op de schepen van Zion", Price = 129.99m, characteristic="Bommen", Image= System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), @"..\", "DataAccessLayer", "Images_For_Database", "emp.jpeg")) }
            };
            context.Products.AddRange(products);

            var parts = new Part[]
            {
                new Part { Name = "Tandwiel", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen"},
                new Part { Name = "M5 Boutje", Description = "Bevestiging van panelen, buizen of interne modules"},
                new Part { Name = "Hydraulische cilinder", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen"},
                new Part { Name = "Koelvloeistofpomp", Description = "Koeling van de motor of elektronische systemen."}
            };
            context.Parts.AddRange(parts);

            var orderProduct = new OrderProduct[] {
                new OrderProduct { Order = orders[0], Product = products[0], Amount = 1 },
                new OrderProduct { Order = orders[0], Product = products[1], Amount = 2 },
                new OrderProduct { Order = orders[1], Product = products[2], Amount = 1 },
                new OrderProduct { Order = orders[2], Product = products[0], Amount = 3 },
                new OrderProduct { Order = orders[3], Product = products[1], Amount = 4 }
            };
            context.AddRange(orderProduct);

            products[0].Parts.Add(parts[0]);
            products[1].Parts.Add(parts[2]);
            products[1].Parts.Add(parts[3]);
            products[2].Parts.Add(parts[0]);
            products[2].Parts.Add(parts[1]);
            products[0].Parts.Add(parts[1]);


            context.SaveChanges();

            context.Database.EnsureCreated();
        }
    }
}
