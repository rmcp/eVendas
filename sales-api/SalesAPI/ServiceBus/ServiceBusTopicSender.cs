using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SalesAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAPI.ServiceBus
{
    public class ServiceBusTopicSender : IServiceBusSender
    {
        private readonly TopicClient _topicClient;
        private readonly IConfiguration _configuration;
        private const string TOPIC_PATH = "sales";
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

        public async Task SendMessage(SaleRealizedMessage sale)
        {
            string data = JsonConvert.SerializeObject(sale);
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
