using PKB.DomainModel;
using PKB.WPF.Common;
using PKB.WPF.Design;
using PKB.WPF.Views.SectionTree;
using SampleData = PKB.WPF.Design.SampleData;

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
            _sectionTreeController.Activate();
            _sectionTreeController.SetResource(SampleData.MakeResource());
        }

        protected override void OnDeactivate()
        {
            _sectionTreeController.Deactivate();
        }
    }
}
