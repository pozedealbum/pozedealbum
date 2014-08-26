using PKB.DomainModel.Common;
using PKB.WPF.Common;
using System.Collections.ObjectModel;

namespace PKB.WPF.Views.SectionTree
{
    public class ResourceViewModel : ViewModelBase
    {
        private string _name;
        private readonly ResourceId _id;
        private readonly ObservableCollection<SectionViewModel> _sections = new ObservableCollection<SectionViewModel>();

        public ResourceViewModel()
            : this(ResourceId.Empty, "")
        {
        }

        public ResourceViewModel(ResourceId id, string name)
        {
            _name = name;
            _id = id;
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public ResourceId Id { get { return _id; } }

        public ObservableCollection<SectionViewModel> Sections { get { return _sections; } }
    }
}