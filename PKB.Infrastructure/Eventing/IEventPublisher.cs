namespace PKB.Infrastructure.Eventing
{

    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent e) where TEvent : IDomainEvent;
    }

    public interface IEventPublisher<TEvent> where TEvent : IDomainEvent
    {
        void Publish(TEvent e);
    }
}
