using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKB.Infrastructure.Messaging
{
    public interface IMessagePublisher
    {
        void Publish<TMessage>(TMessage message) where TMessage : IMessage;
    }

    public interface IMessagePublisher<TMessage> where TMessage : IMessage
    {
        void Publish(TMessage message);
    }
}
