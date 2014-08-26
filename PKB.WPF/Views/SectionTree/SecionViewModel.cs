using PKB.DomainModel.Common;
using PKB.Utility;
using PKB.WPF.Common;
using System.Collections.ObjectModel;

namespace PKB.WPF.Views.SectionTree
{
    public class SectionViewModel : ViewModelBase
    {
        private string _name;
        private readonly SectionId _id;
        private readonly ObservableCollection<SectionViewModel> _subsections = new ObservableCollection<SectionViewModel>();

        public SectionViewModel()
            : this(SectionId.Empty, "")
        {

        }

        public SectionViewModel(SectionId id, string name)
        {
            _name = name;
            _id = id;
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public SectionId Id { get { return _id; } }

        public ObservableCollection<SectionViewModel> Subsections { get { return _subsections; } }
    }
}