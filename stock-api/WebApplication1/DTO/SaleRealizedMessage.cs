using System;

namespace StockAPI.DTO
{
    public class SaleRealizedMessage
    {
        public Guid ProductId { get; set; }

        public int Amount { get; set; }
    }
}
