using Microsoft.Practices.Prism.Mvvm;
using PKB.WPF.Annotations;
using System.Runtime.CompilerServices;

namespace PKB.WPF.Common
{
    public class ViewModelBase : BindableBase
    {
        [NotifyPropertyChangedInvocator]
        protected new virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
    }
}