using System.Windows.Input;
using MVPVM;
using PKB.WPF.Common;

namespace PKB.WPF.Views.SectionTree.EditableSection
{

    public partial class EditableSectionView : AppUserControl<EditableSectionPresenter>
    {
        public EditableSectionView()
        {
            InitializeComponent();
            Loaded += (sender, args) => _sectionNameTextBox.Focus();
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Presenter.ConfirmCommand.Execute();
            }
        }
    }
}
