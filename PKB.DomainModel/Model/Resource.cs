using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKB.DomainModel.Common;
using PKB.Utility;

namespace PKB.DomainModel.Model
{
    public class Resource : AggregateRoot
    {
        private readonly ResourceId _id;
        private string _name;
        private readonly Dictionary<SectionId, Section> _sectionsById = new Dictionary<SectionId, Section>();
        private readonly List<Section> _sections = new List<Section>();

        public Resource(ResourceId id)
            : this(id, "")
        {
        }

        public Resource(ResourceId id, string name)
        {
            _id = id;
            _name = name;
        }


        public Maybe<Section> FindSection(SectionId id)
        {
            Section section;
            return _sectionsById.TryGetValue(id, out section)
                ? section :
                Maybe<Section>.Nothing;
        }

        public IReadOnlyList<Section> Sections { get { return _sections; } }

        public string Name { get { return _name; } }

        public ResourceId Id { get { return _id; } }

        internal void AddSection(Section section)
        {
            _sections.Add(section);
        }

        internal void RemoveSection(Section section)
        {
            _sections.Remove(section);
        }

        internal void RegisterSection(Section section)
        {
            _sectionsById.Add(section.Id, section);
        }

        internal void UnregisterSection(Section section)
        {
            _sectionsById.Remove(section.Id);
        }

    }
}
