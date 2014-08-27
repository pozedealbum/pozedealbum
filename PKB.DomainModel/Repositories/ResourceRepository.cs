using PKB.DomainModel.Common;
using PKB.DomainModel.Model;

namespace PKB.DomainModel.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        public static Resource TestResource { get; private set; }

        static ResourceRepository()
        {
            var book = new Resource(ResourceId.NewId(), "book");
            var preface = new Section(SectionId.NewId(), "preface");
            var part1 = new Section(SectionId.NewId(), "part1");
            var part2 = new Section(SectionId.NewId(), "part2");
            var part3 = new Section(SectionId.NewId(), "part3");
            var chapter1 = new Section(SectionId.NewId(), "chapter1");
            var chapter2 = new Section(SectionId.NewId(), "chapter2");
            var chapter3 = new Section(SectionId.NewId(), "chapter3");
            var section1 = new Section(SectionId.NewId(), "section1");
            var section2 = new Section(SectionId.NewId(), "section2");
            var subSection1 = new Section(SectionId.NewId(), "subSection1");
            var subSection2 = new Section(SectionId.NewId(), "subSection2");

            preface.InsertInside(book);
            part1.InsertInside(book);
            part2.InsertInside(book);
            part3.InsertInside(book);

            chapter1.InsertInside(part1);
            chapter2.InsertInside(part1);
            chapter3.InsertInside(part2);

            section1.InsertInside(chapter3);
            section2.InsertInside(chapter3);

            subSection1.InsertInside(section2);
            subSection2.InsertInside(section2);

            book.ClearEvents();
            TestResource = book;
        }

        public Resource Get(ResourceId id)
        {
            return TestResource;
        }
    }
}
