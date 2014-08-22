using System;

namespace PKB.WPF.Interactivity
{

    public class InteractionRequestedEventArgs : EventArgs
    {
        public InteractionRequestedEventArgs(IInteraction interaction, Type interactionType)
        {
            Interaction = interaction;
            InteractionType = interactionType;
        }

        public IInteraction Interaction { get; private set; }

        public Type InteractionType { get; private set; }

    }
}
