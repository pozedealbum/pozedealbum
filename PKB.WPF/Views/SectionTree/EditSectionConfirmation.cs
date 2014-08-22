using PKB.WPF.Interactivity;

namespace PKB.WPF.Views.SectionTree
{

    public class EditSectionConfirmation : Confirmation
    {
        public readonly SectionViewModel Section;

        public EditSectionConfirmation(SectionViewModel section)
        {
            Section = section;
        }

        public EditSectionConfirmation() : this(new SectionViewModel()) { }
    }
}
