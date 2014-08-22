using System.Windows;

namespace MVPVM
{
    public interface IPresenterAware
    {
        IPresenter Presenter { get; set; }
    }

}
