namespace PKB.Infrastructure.Eventing
{
    public class PublishEventOnMessageBus : IEventPublisher
    {
        private readonly IMessageBus _messageBus;

        public PublishEventOnMessageBus(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Publish<TEvent>(TEvent e) where TEvent : IDomainEvent
        {
            _messageBus.Publish(e);
        }
    }

    public class PublishEventOnMessageBus<T> : IEventPublisher<T>
        where T : IDomainEvent
    {
        private readonly IMessageBus _messageBus;

        public PublishEventOnMessageBus(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Publish(T e)
        {
            _messageBus.Publish(e);
        }
    }
}