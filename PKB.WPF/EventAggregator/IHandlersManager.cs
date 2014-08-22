using System.Collections.Generic;

namespace PKB.WPF.EventAggregator
{
    public interface IHandlersManager
    {
        IHandle<TEvent>[] GetHandlersFor<TEvent>();
    }
}