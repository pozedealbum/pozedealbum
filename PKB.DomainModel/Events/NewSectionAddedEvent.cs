using PKB.DomainModel.Common;

namespace PKB.DomainModel.Events
{
    public class NewSectionAddedEvent
    {
        public readonly SectionId NewSectionId;
        public readonly string NewSectionName;
        public readonly SectionId RootSectionId;
        public readonly SectionId NewSectionParentId;
        public readonly int NewSectionIndex;

        public NewSectionAddedEvent(
            SectionId newSectionId, 
            string newSectionName, 
            SectionId rootSectionId, 
            SectionId newSectionParentId, 
            int newSectionIndex)
        {
            NewSectionId = newSectionId;
            NewSectionName = newSectionName;
            RootSectionId = rootSectionId;
            NewSectionParentId = newSectionParentId;
            NewSectionIndex = newSectionIndex;
        }
    }
}
