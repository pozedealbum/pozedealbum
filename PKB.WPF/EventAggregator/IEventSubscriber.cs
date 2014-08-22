namespace PKB.WPF.EventAggregator
{
    public interface IEventSubscriber
    {
        void Remove<TEvent>(IHandle<TEvent> handler);

        void Add<TEvent>(IHandle<TEvent> handler);
    }
}
