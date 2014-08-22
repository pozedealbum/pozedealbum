using PKB.WPF.Common;

namespace PKB.WPF.Views.SectionTree.EditableSection
{
    public class EditableSectionViewModel : ViewModelBase
    {
        private string _name = string.Empty;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}
