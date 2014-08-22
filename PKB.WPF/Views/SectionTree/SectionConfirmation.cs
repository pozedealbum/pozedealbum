using PKB.WPF.Interactivity;

namespace PKB.WPF.Views.SectionTree
{

    public class SectionConfirmation : Confirmation
    {
        public SectionConfirmation()
            : this("")
        {
        }

        public SectionConfirmation(string sectionName)
        {
            SectionName = sectionName;
        }

        public string SectionName { get; set; }
    }
}
