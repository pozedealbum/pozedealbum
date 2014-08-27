namespace PKB.Infrastructure.Eventing
{
    public class EventSubscriber
    {
        private readonly IMessageBus _messageBus;

        public EventSubscriber(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Subscribe<T>(IEventHandler<T> handler) where T : IDomainEvent
        {
            _messageBus.Subscribe<T>(handler.Handle);
        }

        public void Unsubscribe<T>(IEventHandler<T> handler) where T : IDomainEvent
        {
            _messageBus.Unsubscribe<T>(handler.Handle);
        }
    }
}
