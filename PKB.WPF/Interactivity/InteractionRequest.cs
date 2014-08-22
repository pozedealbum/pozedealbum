using System;

namespace PKB.WPF.Interactivity
{
    public class InteractionRequest<T> : IInteractionRequest
        where T : IInteraction
    {
        public event EventHandler<InteractionRequestedEventArgs> Raised;

        public void Raise(T interaction)
        {
            var handler = Raised;
            if (handler != null)
                handler.Invoke(this, new InteractionRequestedEventArgs(interaction, typeof(T)));
        }
    }
}
