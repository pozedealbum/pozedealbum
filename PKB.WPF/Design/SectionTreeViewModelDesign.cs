using PKB.DomainModel;
using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Design
{
    public class SectionTreeViewModelDesign : SectionTreeViewModel
    {
        public SectionTreeViewModelDesign()
        {
            Resource = SampleData.MakeResource();
            SelectedItem = Resource.Sections[0];
        }
    }
}
