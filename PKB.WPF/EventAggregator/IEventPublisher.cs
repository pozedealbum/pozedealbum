namespace PKB.WPF.EventAggregator
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);
    }

    public interface IEventPublisher<TEvent>
    {
        void Publish(TEvent @event);
    }
}