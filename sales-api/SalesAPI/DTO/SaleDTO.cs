using System;
using System.Collections.Generic;

namespace SalesAPI.DTO
{
    public class SaleDTO
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }

        public decimal Total { get; set; }
    }
}
