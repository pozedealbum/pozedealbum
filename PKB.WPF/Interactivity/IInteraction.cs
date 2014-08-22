using System;

namespace PKB.WPF.Interactivity
{
    public interface IInteraction
    {
        event EventHandler Ending;

        void End();
    }
}
