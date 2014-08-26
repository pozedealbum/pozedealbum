using PKB.DomainModel.Common;
using PKB.DomainModel.Events;
using PKB.Utility;
using System;
using System.Collections.Generic;

namespace PKB.DomainModel.Model
{
    public class Section : IEntity
    {
        private Maybe<Resource> _resource;

        private readonly SectionId _id;
        private string _name;
        private Maybe<Section> _parent;
        private readonly List<Section> _subsections = new List<Section>();

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
            if (_resource.HasValue)
                throw new InvalidOperationException();

            if (!section._resource.HasValue)
                throw new InvalidOperationException();

            _resource.Value.RegisterSection(this);
            section._subsections.Add(section);
            _resource = section._resource;
            _parent = section;

            _resource.Value.Apply(new NewSectionAddedEvent(
               sectionId: Id,
               sectionName: Name,
               resourceId: _resource.Value.Id,
               sectionParentId: _parent.Value.Id,
               sectionIndex: _subsections.Count - 1));
        }

        public void InsertAfter(Section section)
        {
            if (_resource.HasValue)
                throw new InvalidOperationException();

            if (!section._resource.HasValue)
                throw new InvalidOperationException();

            if (!section._parent.HasValue)
                throw new InvalidOperationException();

            _resource.Value.RegisterSection(this);
            int index = section.Index() + 1;
            _resource = section._resource;
            _parent = section._parent;
            _parent.Value._subsections.Insert(index, this);

            _resource.Value.Apply(new NewSectionAddedEvent(
                sectionId: Id,
                sectionName: Name,
                resourceId: _resource.Value.Id,
                sectionParentId: _parent.Value.Id,
                sectionIndex: index));
        }

        public void InsertBefore(Section section)
        {
            if (_resource.HasValue)
                throw new InvalidOperationException();

            if (!section._resource.HasValue)
                throw new InvalidOperationException();

            if (!section._parent.HasValue)
                throw new InvalidOperationException();

            _resource.Value.RegisterSection(this);
            int index = section.Index();
            _resource = section._resource;
            _parent = section._parent;
            _parent.Value._subsections.Insert(index, this);

            _resource.Value.Apply(new NewSectionAddedEvent(
               sectionId: Id,
               sectionName: Name,
               resourceId: _resource.Value.Id,
               sectionParentId: _parent.Value.Id,
               sectionIndex: index));
        }

        public void InsertInside(Resource resource)
        {
            if (_resource.HasValue)
                throw new InvalidOperationException();

            _resource.Value.RegisterSection(this);
            resource.AddSection(this);
            _resource = resource;
            _parent = Maybe<Section>.Nothing;

            _resource.Value.Apply(new NewSectionAddedEvent(
               sectionId: Id,
               sectionName: Name,
               resourceId: _resource.Value.Id,
               sectionParentId: Maybe<SectionId>.Nothing,
               sectionIndex: _subsections.Count - 1));
        }

        public void Remove()
        {
            if (!_resource.HasValue)
                throw new InvalidOperationException();

            if (_parent.HasValue)
                _parent.Value._subsections.Remove(this);
            else
                _resource.Value.RemoveSection(this);

            _resource.Value.Apply(new SectionRemovedEvent(
                sectionId: Id,
                sectionIndex: Index(),
                resourceId: _resource.Value.Id,
                parentSectionId: _parent.HasValue
                    ? _parent.Value.Id
                    : Maybe<SectionId>.Nothing));

            _resource.Value.UnregisterSection(this);
            _resource = Maybe<Resource>.Nothing;
        }

        private int Index()
        {
            return _parent.HasValue
                ? _parent.Value._subsections.IndexOf(this)
                : _resource.Value.Sections.IndexOf(this);
        }
    }
}