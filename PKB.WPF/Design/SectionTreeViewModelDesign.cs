using PKB.Utility;
using PKB.WPF.Views;
using PKB.WPF.Views.Main;
using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Design
{
    public class SectionTreeViewModelDesign : SectionTreeViewModel
    {
        public SectionTreeViewModelDesign()
        {
            Root = new SectionViewModel(SampleData.MakeSection());
            SelectedItem = Root.Subsections[0];
        }
    }
}
