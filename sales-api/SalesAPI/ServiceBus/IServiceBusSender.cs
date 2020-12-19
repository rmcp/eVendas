using SalesAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAPI.ServiceBus
{
    public interface IServiceBusSender
    {
        Task SendMessage(SaleRealizedMessage message);
    }
}
