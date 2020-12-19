using System;

namespace SalesAPI.DTO
{
    public class SaleRealizedMessage
    {
        public Guid ProductId { get; set; }

        public int Amount { get; set; }
    }
}
