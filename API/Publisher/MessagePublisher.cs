using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Publisher
{
    public class MessagePublisher
    {
        // private readonly IQueueClient _queueClient;
        private readonly ITopicClient _topicClient;
        public MessagePublisher(/*IQueueClient queueClient,*/ ITopicClient topicClient)
        {
            //_queueClient = queueClient;
            _topicClient = topicClient;
        }
        public Task Publish<T>(T obj)
        {
            var objAsText = JsonConvert.SerializeObject(obj);
            var message = new Message(body: Encoding.UTF8.GetBytes(objAsText));
            message.UserProperties["messageType"] = typeof(T).Name;
            //return _queueClient.SendAsync(message);
            return _topicClient.SendAsync(message);
        }
        public Task Publish(string raw)
        {
            var message = new Message(body:Encoding.UTF8.GetBytes(raw));
            message.UserProperties["messageType"] = "Raw";
            // return _queueClient.SendAsync(message);
            return _topicClient.SendAsync(message);
        }
    }
}
