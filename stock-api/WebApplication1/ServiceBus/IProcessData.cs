using StockAPI.DTO;

namespace StockAPI.ServiceBus
{
    public interface IProcessData
    {
        void Process(SaleRealizedMessage sale);
    }
}
