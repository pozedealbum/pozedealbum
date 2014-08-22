using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKB.DomainModel;

namespace PKB.WPF.Design
{
    public static class SampleData
    {
        public static Section MakeSection()
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

            return book;
        }
    }
}
