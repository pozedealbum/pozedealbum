using Microsoft.Practices.Prism.Commands;
using PKB.WPF.Common;
using PKB.WPF.Interactivity;

namespace PKB.WPF.Views.SectionTree
{
    public class SectionTreePresenter : Presenter<SectionTreeViewModel>,
        ISectionTreeController
    {
        public DelegateCommand<InsertSectionMode?> AddSectionCommand { get; private set; }

        public DelegateCommand DeleteSectionCommand { get; private set; }

        public InteractionRequest<EditSectionConfirmation> AddSectionRequest { get; private set; }

        public InteractionRequest<EditSectionConfirmation> DeleteSectionRequest { get; private set; }

        public SectionTreePresenter()
        {
            AddSectionCommand = new DelegateCommand<InsertSectionMode?>(AddSection, CanAddSection);
            DeleteSectionCommand = new DelegateCommand(DeleteSection, CanDeleteSection);
            AddSectionRequest = new InteractionRequest<EditSectionConfirmation>();
            DeleteSectionRequest = new InteractionRequest<EditSectionConfirmation>();
        }

        private void AddSection(InsertSectionMode? insertMode)
        {
            var c = new EditSectionConfirmation();
            c.Confirmed += () =>
            {
                switch (insertMode)
                {
                    case InsertSectionMode.After:
                        AddAfter(CurrentSection(), c.Section);
                        break;
                    case InsertSectionMode.Before:
                        AddBefore(CurrentSection(), c.Section);
                        break;
                    case InsertSectionMode.Inside:
                        AddInside(CurrentSection(), c.Section);
                        break;
                }
            };

            AddSectionRequest.Raise(c);
        }

        private SectionViewModel CurrentSection()
        {
            return ViewModel.SelectedItem.ValueOrDefault(ViewModel.Root);
        }

        private bool CanAddSection(InsertSectionMode? insertMode)
        {
            if (!insertMode.HasValue)
                return false;

            if (CurrentSection() == ViewModel.Root)
                return insertMode.Value == InsertSectionMode.Inside;

            return true;
        }

        private void DeleteSection()
        {
            var c = new EditSectionConfirmation(CurrentSection());
            c.Confirmed += () => GetParent(c.Section).Subsections.Remove(c.Section);
            DeleteSectionRequest.Raise(c);
        }

        private bool CanDeleteSection()
        {
            return CurrentSection() != ViewModel.Root;
        }

        public void ChangeRoot(SectionViewModel root)
        {
            ViewModel.SelectedItem = null;
            ViewModel.Root = root;
        }

        private void AddInside(SectionViewModel selectedSection, SectionViewModel newSection)
        {
            selectedSection.Subsections.Add(newSection);
        }

        private void AddAfter(SectionViewModel selectedSection, SectionViewModel newSection)
        {
            GetParent(selectedSection).Subsections.Insert(IndexOf(selectedSection) + 1, newSection);
        }

        private void AddBefore(SectionViewModel selectedSection, SectionViewModel newSection)
        {
            GetParent(selectedSection).Subsections.Insert(IndexOf(selectedSection), newSection);
        }

        public void DragDropInside(SectionViewModel draggedSection, SectionViewModel targetSection)
        {
            RemoveFromParent(draggedSection);
            AddInside(targetSection, draggedSection);
        }

        public void DragDropBefore(SectionViewModel draggedSection, SectionViewModel targetSection)
        {
            RemoveFromParent(draggedSection);
            AddBefore(targetSection, draggedSection);
        }

        public void DragDropAfter(SectionViewModel draggedSection, SectionViewModel targetSection)
        {
            RemoveFromParent(draggedSection);
            AddAfter(targetSection, draggedSection);
        }

        private static void RemoveFromParent(SectionViewModel draggedSection)
        {
            GetParent(draggedSection).Subsections.Remove(draggedSection);
        }

        private static int IndexOf(SectionViewModel targetSection)
        {
            return targetSection.Parent.Value.Subsections.IndexOf(targetSection);
        }

        private static SectionViewModel GetParent(SectionViewModel section)
        {
            return section.Parent.Value;
        }
    }
}