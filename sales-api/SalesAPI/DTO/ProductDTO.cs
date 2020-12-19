using System;

namespace SalesAPI.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
