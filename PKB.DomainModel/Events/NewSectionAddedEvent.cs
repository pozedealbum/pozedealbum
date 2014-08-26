using PKB.DomainModel.Common;
using PKB.Infrastructure.Eventing;
using PKB.Utility;

namespace PKB.DomainModel.Events
{
    public class NewSectionAddedEvent : IDomainEvent
    {
        public readonly SectionId SectionId;
        public readonly string SectionName;
        public readonly ResourceId ResourceId;
        public readonly Maybe<SectionId> SectionParentId;
        public readonly int SectionIndex;

        public NewSectionAddedEvent(
            SectionId sectionId, 
            string sectionName, 
            ResourceId resourceId, 
            Maybe<SectionId> sectionParentId,
            int sectionIndex
            )
        {
            SectionId = sectionId;
            SectionName = sectionName;
            ResourceId = resourceId;
            SectionParentId = sectionParentId;
            SectionIndex = sectionIndex;
        }
    }
}
