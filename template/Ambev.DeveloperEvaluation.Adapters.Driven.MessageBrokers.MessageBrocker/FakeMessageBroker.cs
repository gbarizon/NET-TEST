using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.MessageBroker
{
    public class FakeMessageBroker : IMessageBroker
    {
        public List<object> PublishedEvents { get; } = new();

        public Task PublishAsync<TEvent>(TEvent eventMessage) where TEvent : class
        {
            PublishedEvents.Add(eventMessage);
            return Task.CompletedTask;
        }
    }
}
