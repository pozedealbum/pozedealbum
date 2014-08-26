using PKB.Application.Common;
using PKB.DomainModel;
using PKB.DomainModel.Common;
using PKB.Infrastructure.Commanding;
using PKB.Utility;

namespace PKB.Application.Commands
{
    public class AddNewSectionCommand : ICommand
    {
        public readonly ResourceId ResourceId;

        public readonly string SectionName;

        public readonly InsertSectionMode InsertMode;

        public readonly Maybe<SectionId> CurrentSectionId;

        public AddNewSectionCommand(
            ResourceId resourceId,
            string sectionName,
            InsertSectionMode insertMode,
            Maybe<SectionId> currentSectionId)
        {
            ResourceId = resourceId;
            SectionName = sectionName;
            InsertMode = insertMode;
            CurrentSectionId = currentSectionId;
        }

    }
}
