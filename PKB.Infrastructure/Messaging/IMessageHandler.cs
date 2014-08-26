using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKB.Infrastructure.Messaging
{
    public interface IMessageHandler<TMessage> : IHandler 
        where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}
