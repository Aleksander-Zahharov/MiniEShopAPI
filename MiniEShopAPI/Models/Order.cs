/**
 * Order represents an order entity in the database.
 */
using System;
using System.Collections.Generic;

namespace MiniEShopAPI.Models
{
    public class Order
    {
        public int Id { get; set; } // Unique identifier for the order
        public string CustomerName { get; set; } = string.Empty; // Name of the customer who placed the order
        public DateTime OrderDate { get; set; } // Date when the order was placed
        public List<Product> Products { get; set; } = new List<Product>(); // List of products in the order
        public decimal TotalPrice { get; set; } // Total price of the order
    }
}