using System.Threading.Tasks;

namespace SalesAPI.ServiceBus
{
    public interface IServiceBusTopicSubscription
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
        Task CloseSubscriptionClientAsync();
    }
}
