using System.Windows.Input;

namespace PKB.Infrastructure.Commanding
{
    public class CommandSubscriber
    {
        private readonly IMessageBus _messageBus;

        public CommandSubscriber(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Subscribe<T>(ICommandHandler<T> handler) where T : ICommand
        {
            _messageBus.Subscribe<T>(handler.Handle);
        }

        public void Unsubscribe<T>(ICommandHandler<T> handler) where T : ICommand
        {
            _messageBus.Unsubscribe<T>(handler.Handle);
        }
    }
}
