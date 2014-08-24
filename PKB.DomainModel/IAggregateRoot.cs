using System.Collections.Generic;

namespace PKB.DomainModel
{
    public interface IAggregateRoot : IEntity
    {
        void PublishEvents(IDomainEventPublisher publisher);

        void ClearEvents();
    }
}
