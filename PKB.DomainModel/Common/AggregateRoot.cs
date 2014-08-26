using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKB.Infrastructure.Eventing;

namespace PKB.DomainModel.Common
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private Action<IEventPublisher> _publishEventAction = _ => { };

        public void PublishEvents(IEventPublisher publisher)
        {
            _publishEventAction.Invoke(publisher);
        }

        public void ClearEvents()
        {
            _publishEventAction = _ => { };
        }

        internal void Apply<TEvent>(TEvent e) where TEvent : IDomainEvent
        {
            _publishEventAction += p => p.Publish(e);
        }
    }
}
