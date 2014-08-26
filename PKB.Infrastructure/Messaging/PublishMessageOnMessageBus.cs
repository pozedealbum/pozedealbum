namespace PKB.Infrastructure.Messaging
{
    public class PublishMessageOnMessageBus : IMessagePublisher
    {
        private readonly IMessageBus _messageBus;

        public PublishMessageOnMessageBus(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            _messageBus.Publish(message);
        }
    }

    public class PublishMessageOnMessageBus<TMessage> : IMessagePublisher<TMessage>
        where TMessage : IMessage
    {
        private readonly IMessageBus _messageBus;

        public PublishMessageOnMessageBus(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Publish(TMessage message)
        {
            _messageBus.Publish(message);
        }
    }
}