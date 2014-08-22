using PKB.WPF.Common;

namespace PKB.WPF.Views.Main
{
    public class MainViewModel : ViewModelBase
    {
        private object _sectionTreeView;

        public object SectionTreeView
        {
            get { return _sectionTreeView; }
            set { SetProperty(ref _sectionTreeView, value); }
        }   
    }
}
