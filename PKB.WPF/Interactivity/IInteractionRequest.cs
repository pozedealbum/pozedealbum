using System;

namespace PKB.WPF.Interactivity
{
    public interface IInteractionRequest
    {
        event EventHandler<InteractionRequestedEventArgs> Raised;
    }
}
