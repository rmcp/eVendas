
using StockAPI.DTO;
using System.Threading.Tasks;

namespace StockAPI.ServiceBus
{
    public interface IServiceBusSender
    {
        Task SendMessage(ProductDTO product);
    }
}
