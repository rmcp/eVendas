using System.Threading.Tasks;

namespace StockAPI.ServiceBus
{
    public interface IServiceBusTopicSubscription
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
        Task CloseSubscriptionClientAsync();
    }
}
