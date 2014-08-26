namespace PKB.Infrastructure.Messaging
{
    public class MessageSubscriber
    {
        private readonly IMessageBus _messageBus;

        public MessageSubscriber(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Subscribe<T>(IMessageHandler<T> handler) where T : IMessage
        {
            _messageBus.Subscribe<T>(handler.Handle);
        }

        public void Unsubscribe<T>(IMessageHandler<T> handler) where T : IMessage
        {
            _messageBus.Unsubscribe<T>(handler.Handle);
        }
    }
}
