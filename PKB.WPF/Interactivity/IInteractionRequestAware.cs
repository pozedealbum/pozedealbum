namespace PKB.WPF.Interactivity
{
    public interface IInteractionRequestAware { }

    public interface IInteractionRequestAware<in T> : IInteractionRequestAware
        where T : IInteraction
    {
        void OnInteractionRequested(T interaction);
    }
}
