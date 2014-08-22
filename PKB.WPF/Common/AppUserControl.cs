using System;
using Microsoft.Practices.Prism.Mvvm;
using MVPVM;
using PKB.WPF.Common.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace PKB.WPF.Common
{
    public class AppUserControl : UserControl,
        IHaveDisplayName,
        IView,
        IPresenterAware
    {

        public IPresenter Presenter
        {
            get
            {
                return (IPresenter)GetValue(PresenterProperty);
            }
            set
            {
                SetValue(PresenterProperty, value);
            }
        }

        public static readonly DependencyProperty PresenterProperty =
              DependencyProperty.Register(
              "Presenter",
              typeof(IPresenter),
              typeof(AppUserControl), new PropertyMetadata(null, PresenterChangedCallback));

        private static void PresenterChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var presenter = (IPresenter)args.NewValue;
            ((AppUserControl)dependencyObject).DataContext = presenter.ViewModel;
            presenter.AttachView(dependencyObject);
        }

        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register(
                "DisplayName",
                typeof(string),
                typeof(AppUserControl), new PropertyMetadata(""));


    }

    public class AppUserControl<TPresenter> : AppUserControl
        where TPresenter : class, IPresenter
    {
        public new TPresenter Presenter
        {
            get { return base.Presenter as TPresenter; }
            set { base.Presenter = value; }
        }
    }
}