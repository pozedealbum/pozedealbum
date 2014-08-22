using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKB.WPF.Interactivity;
using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Views.SectionTree.EditableSection
{
    public interface IEditableSectionController : IInteractionRequestAware<SectionConfirmation>
    {
    }
}
