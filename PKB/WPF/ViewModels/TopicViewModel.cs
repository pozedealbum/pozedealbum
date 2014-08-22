using System;
using Microsoft.Practices.Prism.Commands;
using PKB.DomainModel;

namespace PKB.WPF.ViewModels
{
    public class TopicViewModel
    {
        private readonly SectionViewModel _section;

        public Topic Model { get; private set; }

        public TopicViewModel(SectionViewModel section, Topic model)
        {
            _section = section;
            Model = model;
            RemoveTopicCommand = new DelegateCommand(RemoveTopic);
        }

        public DelegateCommand RemoveTopicCommand { get; private set; }

        public string Name
        {
            get { return Model.Name; }
        }

        public void RemoveFromSection(SectionViewModel section)
        {
            section.Topics.Remove(this);
        }


        private void RemoveTopic()
        {
            if (!_section.Topics.Remove(this))
                throw new InvalidOperationException();
        }


    }
}