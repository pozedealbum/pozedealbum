using System.Windows;

namespace MVPVM
{
    public class PresenterAwareProxy : Freezable
    {

        protected override Freezable CreateInstanceCore()
        {
            return new PresenterAwareProxy();
        }

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
           typeof(PresenterAwareProxy), new PropertyMetadata(null));


    }
}
