using System;
using System.ComponentModel;
using System.Windows;
using MVPVM;

namespace PKB.WPF.Common
{

    public static class PresenterLocator
    {


        public static DependencyProperty AutoWirePresenterProperty =
            DependencyProperty.RegisterAttached("AutoWirePresenter", typeof(bool), typeof(PresenterLocator),
            new PropertyMetadata(false, AutoWirePresenterChanged));

        private static void AutoWirePresenterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d))
                return;

            var view = d as IPresenterAware;

            if (view == null)
            {
                throw new InvalidOperationException("Your views must implement IPresenterAware");
            }

            view.Presenter = PresenterLocationProvider.CreatePresenterFromView(view.GetType());
        }


        public static bool GetAutoWirePresenter(DependencyObject obj)
        {
            if (obj != null)
            {
                return (bool)obj.GetValue(AutoWirePresenterProperty);
            }
            return false;
        }


        public static void SetAutoWirePresenter(DependencyObject obj, bool value)
        {
            if (obj != null)
            {
                obj.SetValue(AutoWirePresenterProperty, value);
            }
        }


    }
}
