
using System.Windows;

namespace PKB.WPF.Interactivity.TriggerActions
{

    public class InteractionRequestTrigger : System.Windows.Interactivity.EventTrigger
    {
        private object _sourceObject;


        protected override string GetEventName()
        {
            return "Raised";
        }


        protected override void OnAttached()
        {
            base.OnAttached();

            var element = AssociatedObject as FrameworkElement;
            if (element != null)
            {
                element.Loaded += AssociatedObject_Loaded;
                element.Unloaded += AssociatedObject_Unloaded;
            }
        }


        protected override void OnDetaching()
        {
            base.OnDetaching();

            var element = AssociatedObject as FrameworkElement;
            if (element != null)
            {
                element.Loaded -= AssociatedObject_Loaded;
                element.Unloaded -= AssociatedObject_Unloaded;
            }
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            _sourceObject = SourceObject;
            SourceObject = null;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (SourceObject == null && _sourceObject != null)
            {
                SourceObject = _sourceObject;
            }
        }
    }
}
