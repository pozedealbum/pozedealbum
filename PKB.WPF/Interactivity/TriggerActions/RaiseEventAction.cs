using System;
using System.Windows;
using System.Windows.Interactivity;

namespace PKB.WPF.Interactivity.TriggerActions
{
    public class RaiseEventAction : TriggerAction<FrameworkElement>
    {
        public event EventHandler<InteractionRequestedEventArgs> Handler;

        protected override void Invoke(object parameter)
        {
            if (Handler != null)
                Handler.Invoke(AssociatedObject, (InteractionRequestedEventArgs)parameter);
        }
    }

}
