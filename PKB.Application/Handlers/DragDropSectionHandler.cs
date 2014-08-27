using PKB.Application.Commands;
using PKB.Application.Common;
using PKB.DomainModel.Model;
using PKB.Infrastructure.Commanding;
using PKB.Infrastructure.Eventing;

namespace PKB.Application.Handlers
{
    public class DragDropSectionHandler : ICommandHandler<DragDropSectionCommand>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IEventPublisher _eventPublisher;

        public DragDropSectionHandler(IResourceRepository resourceRepository, IEventPublisher eventPublisher)
        {
            _resourceRepository = resourceRepository;
            _eventPublisher = eventPublisher;
        }

        public void Handle(DragDropSectionCommand command)
        {
            var resource = _resourceRepository.Get(command.ResourceId);
            var section = resource.FindSection(command.SectionId).Value;

            if (command.TargetSectionId.HasValue)
            {
                var targetSection = resource.FindSection(command.TargetSectionId.Value).Value;

                switch (command.InsertMode)
                {
                    case InsertSectionMode.Inside:
                        section.DragDropInside(targetSection);
                        break;
                    case InsertSectionMode.After:
                        section.DragDropAfter(targetSection);
                        break;
                    case InsertSectionMode.Before:
                        section.DragDropBefore(targetSection);
                        break;
                }
            }
            else
            {
                section.DragDropInside(resource);
            }



            resource.PublishEvents(_eventPublisher);
            resource.ClearEvents();
        }
    }
}
