namespace PKB.DomainModel
{
    public interface IDomainEventPublisher
    {
        void Publish<T>(T domainEvent) where T : IDomainEvent;
    }
}
