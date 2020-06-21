using API.Dtos;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Publisher
{
    public class ItemConsumerService : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionClient;
        public ItemConsumerService(ISubscriptionClient subscriptionClient)
        {
            _subscriptionClient = subscriptionClient;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler(handler: (message, token) =>
             {
                 var itemCreated =
                     JsonConvert.DeserializeObject<ItemForServBus>(Encoding.UTF8.GetString(message.Body));

                 Console.WriteLine($"New item with name {itemCreated.Name} and id {itemCreated.Id}");

                 return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
             }, new MessageHandlerOptions(args => Task.CompletedTask)
             {
                 AutoComplete = false,
                 MaxConcurrentCalls = 1
             }) ;

            return Task.CompletedTask;
        }
    }
}
