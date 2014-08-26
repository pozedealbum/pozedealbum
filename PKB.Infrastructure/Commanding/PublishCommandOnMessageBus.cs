namespace PKB.Infrastructure.Commanding
{
    public class PublishCommandOnMessageBus : ICommandPublisher
    {
        private readonly IMessageBus _messageBus;

        public PublishCommandOnMessageBus(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Publish<T>(T command) where T : ICommand
        {
            _messageBus.Publish(command);
        }
    }

    public class PublishCommandOnMessageBus<T> : ICommandPublisher<T>
        where T : ICommand
    {
        private readonly IMessageBus _messageBus;

        public PublishCommandOnMessageBus(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Publish(T command)
        {
            _messageBus.Publish(command);
        }
    }
}