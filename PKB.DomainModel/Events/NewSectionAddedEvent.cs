using System.Collections.Generic;
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
        public readonly IReadOnlyList<SectionId> Parents;
        public readonly int SectionIndex;

        public NewSectionAddedEvent(
            SectionId sectionId,
            string sectionName,
            ResourceId resourceId,
            IReadOnlyList<SectionId> parents,
            int sectionIndex
            )
        {
            SectionId = sectionId;
            SectionName = sectionName;
            ResourceId = resourceId;
            Parents = parents;
            SectionIndex = sectionIndex;
        }
    }
}
