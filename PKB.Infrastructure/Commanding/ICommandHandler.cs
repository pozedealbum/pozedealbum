using System.Windows.Input;

namespace PKB.Infrastructure.Commanding
{
    public interface ICommandHandler<T> : IHandler
        where T : ICommand
    {
        void Handle(T command);
    }
}
