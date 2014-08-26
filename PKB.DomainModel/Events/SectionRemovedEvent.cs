using PKB.DomainModel.Common;
using PKB.Infrastructure.Eventing;
using PKB.Utility;

namespace PKB.DomainModel.Events
{
    public class SectionRemovedEvent : IDomainEvent
    {
        public readonly SectionId SectionId;
        public readonly int SectionIndex;
        public readonly ResourceId ResourceId;
        public readonly Maybe<SectionId> ParentSectionId;

        public SectionRemovedEvent(
            SectionId sectionId,
            int sectionIndex,
            ResourceId resourceId,
            Maybe<SectionId> parentSectionId)
        {
            SectionId = sectionId;
            SectionIndex = sectionIndex;
            ResourceId = resourceId;
            ParentSectionId = parentSectionId;
        }
    }
}
