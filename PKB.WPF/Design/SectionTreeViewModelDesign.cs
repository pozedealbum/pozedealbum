using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Design
{
    public class SectionTreeViewModelDesign : SectionTreeViewModel
    {
        public SectionTreeViewModelDesign()
        {
            Root = SampleData.MakeSection();
            SelectedItem = Root.Subsections[0];
        }
    }
}
