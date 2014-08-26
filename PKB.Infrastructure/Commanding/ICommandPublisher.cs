using System.Windows.Input;

namespace PKB.Infrastructure.Commanding
{
    public interface ICommandPublisher
    {
        void Publish<T>(T command) where T : ICommand;
    }

    public interface ICommandPublisher<T> where T : ICommand
    {
        void Publish(T command);
    }
}
