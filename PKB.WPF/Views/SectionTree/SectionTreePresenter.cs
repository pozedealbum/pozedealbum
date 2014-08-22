using Microsoft.Practices.Prism.Commands;
using PKB.DomainModel;
using PKB.WPF.Common;
using PKB.WPF.Interactivity;

namespace PKB.WPF.Views.SectionTree
{
    public class SectionTreePresenter : Presenter<SectionTreeViewModel>,
        ISectionTreeController
    {
        public DelegateCommand<InsertSectionMode> AddSectionCommand { get; private set; }

        public DelegateCommand DeleteSectionCommand { get; private set; }

        public InteractionRequest<SectionConfirmation> AddSectionRequest { get; private set; }

        public InteractionRequest<SectionConfirmation> DeleteSectionRequest { get; private set; }

        public SectionTreePresenter()
        {
            AddSectionCommand = new DelegateCommand<InsertSectionMode>(AddSection, CanAddSection);
            DeleteSectionCommand = new DelegateCommand(DeleteSection, CanDeleteSection);
            AddSectionRequest = new InteractionRequest<SectionConfirmation>();
            DeleteSectionRequest = new InteractionRequest<SectionConfirmation>();
        }

        private void AddSection(InsertSectionMode insertMode)
        {
            var c = new SectionConfirmation();
            c.Confirmed += () => insertMode.Insert(
                    ViewModel.SelectedItem.ValueOrDefault(ViewModel.Root),
                    new SectionViewModel(new Section(c.SectionName)));

            AddSectionRequest.Raise(c);
        }

        private bool CanAddSection(InsertSectionMode insertMode)
        {
            return ViewModel.SelectedItem.HasValue || insertMode == InsertSectionMode.Inside;
        }

        private void DeleteSection()
        {
            var selectedItem = ViewModel.SelectedItem.Value;

            var c = new SectionConfirmation(selectedItem.Name);
            c.Confirmed += () => selectedItem.Parent.Value.Subsections.Remove(selectedItem);
            DeleteSectionRequest.Raise(c);
        }

        private bool CanDeleteSection()
        {
            return ViewModel.SelectedItem.HasValue && ViewModel.SelectedItem.Value.Parent.HasValue;
        }

        public void ChangeRoot(Section root)
        {
            ViewModel.SelectedItem = null;
            ViewModel.Root = new SectionViewModel(root);
        }

        public void DragDropInside(SectionViewModel draggedSection, SectionViewModel targetSection)
        {
            draggedSection.Parent.Value.Subsections.Remove(draggedSection);
            targetSection.Subsections.Add(draggedSection);
        }

        public void DragDropBefore(SectionViewModel draggedSection, SectionViewModel targetSection)
        {
            draggedSection.Parent.Value.Subsections.Remove(draggedSection);
            int dropPosition = targetSection.Parent.Value.Subsections.IndexOf(targetSection);
            targetSection.Parent.Value.Subsections.Insert(dropPosition, draggedSection);
        }

        public void DragDropAfter(SectionViewModel draggedSection, SectionViewModel targetSection)
        {
            draggedSection.Parent.Value.Subsections.Remove(draggedSection);
            int dropPosition = 1 + targetSection.Parent.Value.Subsections.IndexOf(targetSection);
            targetSection.Parent.Value.Subsections.Insert(dropPosition, draggedSection);
        }
    }
}