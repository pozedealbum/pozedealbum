using PKB.DomainModel.Common;
using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Design
{
    public static class SampleData
    {
        public static ResourceViewModel MakeResource()
        {
            var book = new ResourceViewModel(ResourceId.NewId(), "book");
            var preface = new SectionViewModel(SectionId.NewId(), "preface");
            var part1 = new SectionViewModel(SectionId.NewId(), "part1");
            var part2 = new SectionViewModel(SectionId.NewId(), "part2");
            var part3 = new SectionViewModel(SectionId.NewId(), "part3");
            var chapter1 = new SectionViewModel(SectionId.NewId(), "chapter1");
            var chapter2 = new SectionViewModel(SectionId.NewId(), "chapter2");
            var chapter3 = new SectionViewModel(SectionId.NewId(), "chapter3");
            var section1 = new SectionViewModel(SectionId.NewId(), "section1");
            var section2 = new SectionViewModel(SectionId.NewId(), "section2");
            var subSection1 = new SectionViewModel(SectionId.NewId(), "subSection1");
            var subSection2 = new SectionViewModel(SectionId.NewId(), "subSection2");

            part1.Subsections.Add(chapter1);
            part1.Subsections.Add(chapter2);
            part2.Subsections.Add(chapter3);
            chapter3.Subsections.Add(section1);
            chapter3.Subsections.Add(section2);
            section2.Subsections.Add(subSection1);
            section2.Subsections.Add(subSection2);

            book.Sections.Add(preface);
            book.Sections.Add(part1);
            book.Sections.Add(part2);
            book.Sections.Add(part3);

            return book;
        }
    }
}
