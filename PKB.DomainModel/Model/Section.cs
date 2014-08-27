using System.Linq;
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


            section._subsections.Add(this);
            _resource = section._resource;
            _parent = section;
            _resource.Value.RegisterSection(this);

            _resource.Value.Apply(CreateNewSectionAddedEvent());
        }

        public void InsertAfter(Section section)
        {
            if (_resource.HasValue)
                throw new InvalidOperationException();

            if (!section._resource.HasValue)
                throw new InvalidOperationException();

            if (!section._parent.HasValue)
                throw new InvalidOperationException();


            int index = section.Index() + 1;
            _resource = section._resource;
            _parent = section._parent;
            _parent.Value._subsections.Insert(index, this);
            _resource.Value.RegisterSection(this);

            _resource.Value.Apply(CreateNewSectionAddedEvent());
        }

        public void InsertBefore(Section section)
        {
            if (_resource.HasValue)
                throw new InvalidOperationException();

            if (!section._resource.HasValue)
                throw new InvalidOperationException();

            if (!section._parent.HasValue)
                throw new InvalidOperationException();


            int index = section.Index();
            _resource = section._resource;
            _parent = section._parent;
            _parent.Value._subsections.Insert(index, this);
            _resource.Value.RegisterSection(this);

            _resource.Value.Apply(CreateNewSectionAddedEvent());
        }

        private NewSectionAddedEvent CreateNewSectionAddedEvent()
        {
            return new NewSectionAddedEvent(
                sectionId: Id,
                sectionName: Name,
                resourceId: _resource.Value.Id,
                parents: GetParents().ToList(),
                sectionIndex: Index());
        }

        public void InsertInside(Resource resource)
        {
            if (_resource.HasValue)
                throw new InvalidOperationException();


            resource.AddSection(this);
            _resource = resource;
            _parent = Maybe<Section>.Nothing;
            _resource.Value.RegisterSection(this);

            _resource.Value.Apply(CreateNewSectionAddedEvent());
        }

        public void Remove()
        {
            if (!_resource.HasValue)
                throw new InvalidOperationException();

            _resource.Value.Apply(CreateSectionRemovedEvent());

            if (_parent.HasValue)
                _parent.Value._subsections.Remove(this);
            else
                _resource.Value.RemoveSection(this);

            _resource.Value.UnregisterSection(this);
            _resource = Maybe<Resource>.Nothing;
        }

        public void DragDropInside(Resource resource)
        {
            _parent.Value._subsections.Remove(this);
            resource.AddSection(this);
            _parent = Maybe<Section>.Nothing;
        }

        public void DragDropInside(Section section)
        {
            _parent.Value._subsections.Remove(this);
            section._subsections.Add(this);
            _parent = section;
        }

        public void DragDropBefore(Section section)
        {
            _parent.Value._subsections.Remove(this);
            section._parent.Value._subsections.Insert(section.Index(), this);
        }

        public void DragDropAfter(Section section)
        {

        }


        private SectionRemovedEvent CreateSectionRemovedEvent()
        {
            return new SectionRemovedEvent(
                sectionId: Id,
                sectionIndex: Index(),
                resourceId: _resource.Value.Id,
                parents: GetParents().ToList());
        }

        private int Index()
        {
            return _parent.HasValue
                ? _parent.Value._subsections.IndexOf(this)
                : _resource.Value.Sections.IndexOf(this);
        }

        private IEnumerable<SectionId> GetParents()
        {
            var parent = _parent;

            while (parent.HasValue)
            {
                yield return parent.Value.Id;
                parent = parent.Value._parent;
            }
        }
    }
}