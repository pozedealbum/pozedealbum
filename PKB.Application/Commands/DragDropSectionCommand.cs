using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKB.Application.Common;
using PKB.DomainModel.Common;
using PKB.Infrastructure.Commanding;
using PKB.Utility;

namespace PKB.Application.Commands
{
    public class DragDropSectionCommand : ICommand
    {
        public readonly ResourceId ResourceId;

        public readonly SectionId SectionId;

        public readonly InsertSectionMode InsertMode;

        public readonly Maybe<SectionId> TargetSectionId;

        public DragDropSectionCommand(
            ResourceId resourceId, 
            SectionId sectionId,
            InsertSectionMode insertMode, 
            Maybe<SectionId> targetSectionId)
        {
            ResourceId = resourceId;
            SectionId = sectionId;
            InsertMode = insertMode;
            TargetSectionId = targetSectionId;
        }
    }
}
