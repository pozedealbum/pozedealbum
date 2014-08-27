using System.Collections.Generic;
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
        public readonly IReadOnlyList<SectionId> Parents;

        public SectionRemovedEvent(
            SectionId sectionId,
            int sectionIndex,
            ResourceId resourceId,
            IReadOnlyList<SectionId> parents)
        {
            SectionId = sectionId;
            SectionIndex = sectionIndex;
            ResourceId = resourceId;
            Parents = parents;
        }
    }
}
