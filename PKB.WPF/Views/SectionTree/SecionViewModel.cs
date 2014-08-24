using PKB.Utility;
using PKB.WPF.Common;
using System.Collections.ObjectModel;

namespace PKB.WPF.Views.SectionTree
{
    public class SectionViewModel : ViewModelBase
    {
        private string _name;
        private readonly ObservableCollection<SectionViewModel> _subsections = new ObservableCollection<SectionViewModel>();

        public SectionViewModel()
            : this("")
        {
        }

        public SectionViewModel(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public ObservableCollection<SectionViewModel> Subsections { get { return _subsections; } }
    }
}