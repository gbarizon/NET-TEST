using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.MessageBroker
{
    public interface IMessageBroker
    {        
        Task PublishAsync<TEvent>(TEvent eventMessage) where TEvent : class;
    }
}
