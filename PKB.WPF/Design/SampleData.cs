using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Design
{
    public static class SampleData
    {
        public static SectionViewModel MakeSection()
        {
            var book = new SectionViewModel("book");
            var preface = new SectionViewModel("preface");
            var part1 = new SectionViewModel("part1");
            var part2 = new SectionViewModel("part2");
            var part3 = new SectionViewModel("part3");
            var chapter1 = new SectionViewModel("chapter1");
            var chapter2 = new SectionViewModel("chapter2");
            var chapter3 = new SectionViewModel("chapter3");
            var section1 = new SectionViewModel("section1");
            var section2 = new SectionViewModel("section2");
            var subSection1 = new SectionViewModel("subSection1");
            var subSection2 = new SectionViewModel("subSection2");

            part1.Subsections.Add(chapter1);
            part1.Subsections.Add(chapter2);
            part2.Subsections.Add(chapter3);
            chapter3.Subsections.Add(section1);
            chapter3.Subsections.Add(section2);
            section2.Subsections.Add(subSection1);
            section2.Subsections.Add(subSection2);

            book.Subsections.Add(preface);
            book.Subsections.Add(part1);
            book.Subsections.Add(part2);
            book.Subsections.Add(part3);

            return book;
        }
    }
}
