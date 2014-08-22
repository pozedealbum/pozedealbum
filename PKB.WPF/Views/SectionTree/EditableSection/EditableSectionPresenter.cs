using Microsoft.Practices.Prism.Commands;
using PKB.WPF.Common;
using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Views.SectionTree.EditableSection
{
    public class EditableSectionPresenter : Presenter<EditableSectionViewModel>,
        IEditableSectionController
    {
        private SectionConfirmation _interaction;

        public DelegateCommand ConfirmCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        public EditableSectionPresenter()
        {
            ConfirmCommand = new DelegateCommand(Confirm, CanConfirm);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private bool CanConfirm()
        {
            return !string.IsNullOrWhiteSpace(ViewModel.Name);
        }

        private void Confirm()
        {
            _interaction.SectionName = ViewModel.Name.Trim();
            _interaction.Confirm();
        }

        private void Cancel()
        {
            _interaction.Cancel();
        }

        public void OnInteractionRequested(SectionConfirmation interaction)
        {
            _interaction = interaction;
            ViewModel.Name = _interaction.SectionName;
        }
    }
}