using System;

namespace SalesAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }

    }
}
