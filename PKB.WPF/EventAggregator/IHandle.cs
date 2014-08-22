using System.Collections;
using System.Collections.Generic;

namespace PKB.WPF.EventAggregator
{
    public interface IHandle { }

    public interface IHandle<in TEvent> : IHandle
    {
        void Handle(TEvent e);
    }
}