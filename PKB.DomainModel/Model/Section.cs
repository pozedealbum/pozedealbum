using PKB.DomainModel.Common;
using PKB.Utility;
using System;
using System.Collections.Generic;

namespace PKB.DomainModel.Model
{
    public class Section : IEntity
    {
        internal Maybe<RootSection> Root { get; set; }

        private readonly SectionId _id;
        private string _name;
        private Maybe<Section> _parent;
        private readonly List<Section> _subsections = new List<Section>();

        public Section(SectionId id)
            : this(id, "")
        {
        }

        public Section(SectionId id, string name)
        {
            _id = id;
            _name = name;
        }

        public SectionId Id { get { return _id; } }

        public string Name { get { return _name; } }

        public IReadOnlyList<Section> Subsections { get { return _subsections; } }



        public void InsertInside(Section section)
        {
            if (_parent.HasValue)
                throw new InvalidOperationException();

            section._subsections.Add(section);
            _parent = section;
        }

        public void InsertAfter(Section section)
        {
            if (_parent.HasValue)
                throw new InvalidOperationException();

            if (!section._parent.HasValue)
                throw new InvalidOperationException();

            _parent = section._parent;
            _parent.Value._subsections.Insert(section.Index() + 1, this);
        }

        public void InsertBefore(Section section)
        {
            if (_parent.HasValue)
                throw new InvalidOperationException();

            if (!section._parent.HasValue)
                throw new InvalidOperationException();

            _parent = section._parent;
            _parent.Value._subsections.Insert(section.Index(), this);
        }

        private int Index()
        {
            return _parent.HasValue ? _parent.Value._subsections.IndexOf(this) : 0;
        }
    }
}