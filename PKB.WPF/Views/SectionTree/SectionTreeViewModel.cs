using PKB.Utility;
using PKB.WPF.Common;

namespace PKB.WPF.Views.SectionTree
{
    public class SectionTreeViewModel : ViewModelBase
    {
        private Maybe<SectionViewModel> _selectedItem;
        private ResourceViewModel _resource = new ResourceViewModel();

        public ResourceViewModel Resource
        {
            get { return _resource; }
            set { SetProperty(ref _resource, value); }
        }

        public Maybe<SectionViewModel> SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
    }
}