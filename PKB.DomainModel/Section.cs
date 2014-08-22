using System.Collections.Generic;

namespace PKB.DomainModel
{
    public class Section
    {
        private readonly List<Section> _subsections = new List<Section>();

        public Section(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public IReadOnlyList<Section> Subsections { get { return _subsections; } }

        public void InsertSubsection(int index, Section section)
        {
            _subsections.Insert(index, section);
        }

        public void RemoveSubsectionAt(int index)
        {
            _subsections.RemoveAt(index);
        }

        public void AddSubsections(params Section[] sections)
        {
            _subsections.AddRange(sections);
        }

    }
}