namespace PKB.WPF.Views.SectionTree
{
    public abstract class InsertSectionMode
    {
        public static readonly InsertSectionMode Inside = new InsideInsertMode();
        public static readonly InsertSectionMode After = new AfterInsertMode();
        public static readonly InsertSectionMode Before = new BeforeInsertMode();

        public abstract void Insert(SectionViewModel selectedSection, SectionViewModel newSection);

        private InsertSectionMode()
        {

        }

        private class InsideInsertMode : InsertSectionMode
        {
            public override void Insert(SectionViewModel selectedSection, SectionViewModel newSection)
            {
                selectedSection.Subsections.Add(newSection);
            }
        }

        private class AfterInsertMode : InsertSectionMode
        {

            public override void Insert(SectionViewModel selectedSection, SectionViewModel newSection)
            {
                selectedSection.Parent.Value.Subsections.Insert(IndexOf(selectedSection) + 1, newSection);
            }
        }

        private class BeforeInsertMode : InsertSectionMode
        {
            public override void Insert(SectionViewModel selectedSection, SectionViewModel newSection)
            {
                selectedSection.Parent.Value.Subsections.Insert(IndexOf(selectedSection), newSection);
            }
        }

        private static int IndexOf(SectionViewModel viewModel)
        {
            return viewModel.Parent.HasValue
                    ? viewModel.Parent.Value.Subsections.IndexOf(viewModel)
                    : 0;
        }

    }
}
