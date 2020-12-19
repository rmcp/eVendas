using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockAPI.DTO;
using System;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.ServiceBus
{
    public class ServiceBusTopicSender : IServiceBusSender
    {
        private readonly TopicClient _topicClient;
        private readonly IConfiguration _configuration;
        private const string TOPIC_PATH = "stock";
        private readonly ILogger _logger;
            
        public ServiceBusTopicSender(IConfiguration configuration,
            ILogger<ServiceBusTopicSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _topicClient = new TopicClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                TOPIC_PATH
            );
        }

        public async Task SendMessage(ProductDTO product)
        {
            string data = JsonConvert.SerializeObject(product);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            try
            {
                await _topicClient.SendAsync(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
