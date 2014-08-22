using System;
using System.Windows;
using System.Windows.Interactivity;

namespace PKB.WPF.Interactivity.TriggerActions
{
    public class PopupWindowAction : TriggerAction<FrameworkElement>
    {
        public object WindowContent { get; set; }

        public Type WindowContentType { get; set; }

        public bool IsModal { get; set; }

        public PopupWindowAction()
        {
            IsModal = true;
        }

        protected override void Invoke(object parameter)
        {
            EnsureOnlyOneContentPropertyIsSetted();
            PopupWindow.Popup(
                GetWindowContent(),
                (InteractionRequestedEventArgs)parameter,
                IsModal);
        }

        private void EnsureOnlyOneContentPropertyIsSetted()
        {
            int propertiesSettedCount = 0;
            if (WindowContent != null)
                propertiesSettedCount++;
            if (WindowContentType != null)
                propertiesSettedCount++;

            if (propertiesSettedCount != 1)
                throw new InvalidOperationException();
        }



        private object GetWindowContent()
        {
            return WindowContent ??
                   CreateWindowFromContentType();
        }

        private object CreateWindowFromContentType()
        {
            return WindowContentType != null
                ? Activator.CreateInstance(WindowContentType)
                : null;
        }
    }
}