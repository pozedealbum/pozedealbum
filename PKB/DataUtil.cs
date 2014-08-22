using System.Collections.ObjectModel;
using PKB.DomainModel;
using PKB.WPF.ViewModels;

namespace PKB
{
    public static class DataUtil
    {
        public static ObservableCollection<SectionViewModel> MakeSections()
        {
            var book = new Section("book");
            var preface = new Section("preface");
            var part1 = new Section("part1");
            var part2 = new Section("part2");
            var part3 = new Section("part3");
            var chapter1 = new Section("chapter1");
            var chapter2 = new Section("chapter2");
            var chapter3 = new Section("chapter3");
            var section1 = new Section("section1");
            var section2 = new Section("section2");
            var subSection1 = new Section("subSection1");
            var subSection2 = new Section("subSection2");

            part1.AddSubsections(chapter1);
            part1.AddSubsections(chapter2);
            part2.AddSubsections(chapter3);
            chapter3.AddSubsections(section1);
            chapter3.AddSubsections(section2);
            section2.AddSubsections(subSection1);
            section2.AddSubsections(subSection2);

            book.AddSubsections(preface);
            book.AddSubsections(part1);
            book.AddSubsections(part2);
            book.AddSubsections(part3);

            var topic1 = new Topic("topic1");
            var topic2 = new Topic("topic2");
            var topic3 = new Topic("topic3");
            var topic4 = new Topic("topic4");
            var topic5 = new Topic("topic5");
            var topic6 = new Topic("topic6");

            part1.AddTopics(topic6);
            chapter1.AddTopics(topic1, topic2, topic3);
            subSection1.AddTopics(topic4);
            section2.AddTopics(topic1, topic4);

            var sections = new ObservableCollection<SectionViewModel>();
            sections.Add(new SectionViewModel(book));
            return sections;
        }
    }
}
