using PKB.Utility;

namespace PKB.WPF.EventAggregator
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IHandlersManager _manager;

        public EventPublisher(IHandlersManager manager)
        {
            _manager = manager;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            _manager.GetHandlersFor<TEvent>()
                .ForEach(x => x.Handle(@event));
        }
    }

    public class EventPublisher<TEvent> : IEventPublisher<TEvent>
    {
        private readonly IHandlersManager _manager;

        public EventPublisher(IHandlersManager manager)
        {
            _manager = manager;
        }

        public void Publish(TEvent @event)
        {
             _manager.GetHandlersFor<TEvent>()
                .ForEach(x => x.Handle(@event));
        }
    }
}