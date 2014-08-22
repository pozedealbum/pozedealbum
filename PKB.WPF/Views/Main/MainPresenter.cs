using PKB.WPF.Common;
using PKB.WPF.Design;
using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Views.Main
{
    public class MainPresenter : Screen<MainViewModel>
    {
        private readonly ISectionTreeController _sectionTreeController;

        public MainPresenter(
            ISectionTreeController sectionTreeController)
        {
            _sectionTreeController = sectionTreeController;
            ViewModel.SectionTreeView = _sectionTreeController.View;
        }

        protected override void OnActivate()
        {
            _sectionTreeController.ChangeRoot(SampleData.MakeSection());
        }
    }
}
