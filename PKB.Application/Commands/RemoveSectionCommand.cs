using PKB.DomainModel.Common;
using PKB.Infrastructure.Commanding;

namespace PKB.Application.Commands
{
    public class RemoveSectionCommand : ICommand
    {
        public readonly ResourceId ResourceId;


        public readonly SectionId SectionId;

        public RemoveSectionCommand(ResourceId resourceId, SectionId sectionId)
        {
            ResourceId = resourceId;
            SectionId = sectionId;
        }
    }
}
