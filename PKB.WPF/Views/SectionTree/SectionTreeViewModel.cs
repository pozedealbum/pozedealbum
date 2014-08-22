using PKB.Utility;
using PKB.WPF.Common;

namespace PKB.WPF.Views.SectionTree
{
    public class SectionTreeViewModel : ViewModelBase
    {
        private Maybe<SectionViewModel> _selectedItem;
        private SectionViewModel _root = new SectionViewModel();

        public SectionViewModel Root
        {
            get { return _root; }
            set { SetProperty(ref _root, value); }
        }

        public Maybe<SectionViewModel> SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
    }
}