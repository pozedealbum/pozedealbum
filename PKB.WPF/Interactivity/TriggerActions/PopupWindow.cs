using System;
using System.Windows;
using MVPVM;
using PKB.Utility;
using PKB.WPF.Common.Interfaces;

namespace PKB.WPF.Interactivity.TriggerActions
{
    public static class PopupWindow
    {
        private static readonly string OnInteractionRequestedMethodName =
            StaticReflection.GetMember<IInteractionRequestAware<IInteraction>>(
                x => x.OnInteractionRequested(null)).Name;


        public static void Popup(object windowContent, InteractionRequestedEventArgs args, bool isModal = true)
        {
            var window = MakeWrapperWindow();
            window.Content = windowContent;
            SetTitle(windowContent, window);
            SetHandlers(window, args);
            SetInteractionFor(windowContent, args);

            if (isModal)
                window.ShowDialog();
            else
                window.Show();
        }

        private static void SetTitle(object windowContent, Window window)
        {
            var haveDisplayName = windowContent as IHaveDisplayName;
            if (haveDisplayName != null)
                window.Title = haveDisplayName.DisplayName;
        }

        private static void SetHandlers(Window window, InteractionRequestedEventArgs args)
        {
            EventHandler closedHandler = null;
            closedHandler = (o, e) =>
            {
                window.Closed -= closedHandler;
                window.Content = null;
                args.Interaction.End();
            };
            window.Closed += closedHandler;

            EventHandler endingHandler = null;
            endingHandler = (o, e) =>
            {
                window.Closed -= closedHandler;
                args.Interaction.Ending -= endingHandler;
                window.Content = null;
                window.Close();
            };
            args.Interaction.Ending += endingHandler;
        }

        private static void SetInteractionFor(object windowContent, InteractionRequestedEventArgs args)
        {
            TrySetInteractionFor(windowContent, args);

            var frameworkElement = windowContent as FrameworkElement;
            if (frameworkElement != null)
                TrySetInteractionFor(frameworkElement.DataContext, args);


            var presenterAware = windowContent as IPresenterAware;
            if (presenterAware != null)
                TrySetInteractionFor(presenterAware.Presenter, args);
        }

        private static Window MakeWrapperWindow()
        {
            return new Window()
            {
                ResizeMode = ResizeMode.NoResize,
                ShowInTaskbar = false,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
        }

        private static void TrySetInteractionFor(object value, InteractionRequestedEventArgs args)
        {
            if (!(value is IInteractionRequestAware))
                return;

            var type = typeof(IInteractionRequestAware<>).MakeGenericType(args.InteractionType);
            if (!type.IsInstanceOfType(value))
                throw new InvalidOperationException();

            type.GetMethod(OnInteractionRequestedMethodName).Invoke(value, new object[] { args.Interaction });
        }
    }
}
