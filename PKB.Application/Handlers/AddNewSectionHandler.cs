using PKB.Application.Commands;
using PKB.Application.Common;
using PKB.DomainModel;
using PKB.DomainModel.Common;
using PKB.DomainModel.Model;
using PKB.Infrastructure.Commanding;

namespace PKB.Application.Handlers
{
    class AddNewSectionHandler : ICommandHandler<AddNewSectionCommand>
    {
        private readonly IResourceRepository _resourceRepository;

        public AddNewSectionHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public void Handle(AddNewSectionCommand command)
        {
            var resource = _resourceRepository.Get(command.ResourceId);

            var newSection = new Section(SectionId.NewId(), command.SectionName);

            if (command.CurrentSectionId.HasValue)
            {
                var currentSection = resource.FindSection(command.CurrentSectionId.Value).Value;

                switch (command.InsertMode)
                {
                    case InsertSectionMode.Inside:
                        newSection.InsertInside(currentSection);
                        break;
                    case InsertSectionMode.After:
                        newSection.InsertAfter(currentSection);
                        break;
                    case InsertSectionMode.Before:
                        newSection.InsertBefore(currentSection);
                        break;
                }
            }
            else
            {
                newSection.InsertInside(resource);
            }


        }

    }
}
