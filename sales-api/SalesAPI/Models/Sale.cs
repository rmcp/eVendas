using System;
using System.Collections.Generic;

namespace SalesAPI.Models
{
    public class Sale
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public decimal Total { get; set; }
    }
}
