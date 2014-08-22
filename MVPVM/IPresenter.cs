using System;

namespace MVPVM
{
    public interface IPresenter
    {
        void AttachView(object view);

        object ViewModel { get; }
    }

}
