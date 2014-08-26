using PKB.Infrastructure.Messaging;

namespace PKB.Infrastructure.Eventing
{
    public interface IEventHandler<TEvent> : IHandler
        where TEvent : IDomainEvent
    {
        void Handle(TEvent e);
    }
}
