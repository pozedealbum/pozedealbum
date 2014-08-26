using Microsoft.Practices.Prism.Commands;
using PKB.Application;
using PKB.Application.Commands;
using PKB.Application.Common;
using PKB.DomainModel.Common;
using PKB.Infrastructure.Commanding;
using PKB.Utility;
using PKB.WPF.Common;
using PKB.WPF.Interactivity;

namespace PKB.WPF.Views.SectionTree
{
    public class SectionTreePresenter : Presenter<SectionTreeViewModel>,
        ISectionTreeController
    {
        private readonly ICommandHandler<AddNewSectionCommand> _addNewSectionHandler;
        private readonly ICommandHandler<RemoveSectionCommand> _removeSectionHandler;
        public DelegateCommand<InsertSectionMode?> AddSectionCommand { get; private set; }

        public DelegateCommand DeleteSectionCommand { get; private set; }

        public InteractionRequest<EditSectionConfirmation> AddSectionRequest { get; private set; }

        public InteractionRequest<EditSectionConfirmation> DeleteSectionRequest { get; private set; }

        public SectionTreePresenter(
            ICommandHandler<AddNewSectionCommand> addNewSectionHandler,
            ICommandHandler<RemoveSectionCommand> removeSectionHandler)
        {
            _addNewSectionHandler = addNewSectionHandler;
            _removeSectionHandler = removeSectionHandler;
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
                _addNewSectionHandler.Handle(new AddNewSectionCommand(
                    resourceId: ViewModel.Resource.Id,
                    sectionName: c.Section.Name,
                    insertMode: insertMode.Value,
                    currentSectionId: ViewModel.SelectedItem.HasValue
                        ? ViewModel.SelectedItem.Value.Id
                        : Maybe<SectionId>.Nothing));
            };

            AddSectionRequest.Raise(c);
        }

        private bool CanAddSection(InsertSectionMode? insertMode)
        {
            if (!insertMode.HasValue)
                return false;

            if (!ViewModel.SelectedItem.HasValue)
                return insertMode.Value == InsertSectionMode.Inside;

            return true;
        }

        private void DeleteSection()
        {
            var c = new EditSectionConfirmation(ViewModel.SelectedItem.Value);
            c.Confirmed += () => _removeSectionHandler.Handle(
                new RemoveSectionCommand(
                    resourceId: ViewModel.Resource.Id,
                    sectionId: ViewModel.SelectedItem.Value.Id));

            DeleteSectionRequest.Raise(c);
        }

        private bool CanDeleteSection()
        {
            return ViewModel.SelectedItem.HasValue;
        }

        public void SetResource(ResourceViewModel resource)
        {
            ViewModel.SelectedItem = null;
            ViewModel.Resource = resource;
        }

        //private void AddInside(SectionViewModel selectedSection, SectionViewModel newSection)
        //{
        //    selectedSection.Sections.Add(newSection);
        //}

        //private void AddAfter(SectionViewModel selectedSection, SectionViewModel newSection)
        //{
        //    GetParent(selectedSection).Sections.Insert(IndexOf(selectedSection) + 1, newSection);
        //}

        //private void AddBefore(SectionViewModel selectedSection, SectionViewModel newSection)
        //{
        //    GetParent(selectedSection).Sections.Insert(IndexOf(selectedSection), newSection);
        //}

        public void DragDropInside(SectionViewModel draggedSection, Maybe<SectionViewModel> targetSection)
        {
            //RemoveFromParent(draggedSection);
            //AddInside(targetSection, draggedSection);
        }

        public void DragDropBefore(SectionViewModel draggedSection, Maybe<SectionViewModel> targetSection)
        {
            //RemoveFromParent(draggedSection);
            //AddBefore(targetSection, draggedSection);
        }

        public void DragDropAfter(SectionViewModel draggedSection, Maybe<SectionViewModel> targetSection)
        {
            //RemoveFromParent(draggedSection);
            //AddAfter(targetSection, draggedSection);
        }

        //private static void RemoveFromParent(SectionViewModel draggedSection)
        //{
        //    GetParent(draggedSection).Sections.Remove(draggedSection);
        //}

        //private static int IndexOf(SectionViewModel targetSection)
        //{
        //    return targetSection.Parent.Value.Sections.IndexOf(targetSection);
        //}

        //private static SectionViewModel GetParent(SectionViewModel section)
        //{
        //    return section.Parent.Value;
        //}
    }
}