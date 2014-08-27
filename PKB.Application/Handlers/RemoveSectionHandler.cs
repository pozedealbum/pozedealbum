using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKB.Application.Commands;
using PKB.DomainModel.Common;
using PKB.DomainModel.Model;
using PKB.Infrastructure.Commanding;
using PKB.Infrastructure.Eventing;

namespace PKB.Application.Handlers
{
    public class RemoveSectionHandler : ICommandHandler<RemoveSectionCommand>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IEventPublisher _eventPublisher;

        public RemoveSectionHandler(IResourceRepository resourceRepository, IEventPublisher eventPublisher)
        {
            _resourceRepository = resourceRepository;
            _eventPublisher = eventPublisher;
        }

        public void Handle(RemoveSectionCommand command)
        {
            var resource = _resourceRepository.Get(command.ResourceId);
            var section = resource.FindSection(command.SectionId).Value;
            section.Remove();
            resource.PublishEvents(_eventPublisher);
            resource.ClearEvents();
        }
    }
}
