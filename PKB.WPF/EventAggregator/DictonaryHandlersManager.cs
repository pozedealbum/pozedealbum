using PKB.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PKB.WPF.EventAggregator
{
    public class DictonaryHandlersManager : IHandlersManager, IEventSubscriber
    {
        private readonly ConcurrentDictionary<Type, HashSet<object>> _handlers = new ConcurrentDictionary<Type, HashSet<object>>();

        public IHandle<TEvent>[] GetHandlersFor<TEvent>()
        {
            var handlers = _handlers.GetOrAdd(typeof(TEvent), new HashSet<object>());

            IHandle<TEvent>[] result;
            lock (handlers)
            {
                result = handlers.Cast<IHandle<TEvent>>().ToArray();
            }
            return result;
        }

        public void Add<T>(IHandle<T> handler)
        {
            _handlers.AddOrUpdate(typeof(T),
                type => new HashSet<object>(new[] { handler }),
                (type, p) =>
                {
                    lock (p)
                    {
                        p.Add(handler);
                    }
                    return p;
                });
        }

        public void Remove<T>(IHandle<T> handler)
        {
            _handlers.AddOrUpdate(typeof(T),
                type => new HashSet<object>(),
                (type, p) =>
                {
                    lock (p)
                    {
                        if (!(p.Remove(handler)))
                            throw new InvalidOperationException();
                    }
                    return p;
                });
        }

    }
}