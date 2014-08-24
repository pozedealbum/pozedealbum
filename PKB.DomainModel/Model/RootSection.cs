using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKB.DomainModel.Common;
using PKB.Utility;

namespace PKB.DomainModel.Model
{
    public class RootSection : Section, IAggregateRoot
    {
        private Action<IDomainEventPublisher> _publishEventAction = _ => { };

        public RootSection(SectionId id)
            : base(id)
        {
        }

        public RootSection(SectionId id, string name)
            : base(id, name)
        {
        }

        public void PublishEvents(IDomainEventPublisher publisher)
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

        public Maybe<Section> FindSection(SectionId currentSectionId)
        {
            throw new NotImplementedException();
        }

    }
}
