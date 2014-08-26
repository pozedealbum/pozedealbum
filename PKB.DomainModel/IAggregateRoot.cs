using System.Collections.Generic;
using PKB.Infrastructure.Eventing;

namespace PKB.DomainModel
{
    public interface IAggregateRoot : IEntity
    {
        void PublishEvents(IEventPublisher publisher);

        void ClearEvents();
    }
}
