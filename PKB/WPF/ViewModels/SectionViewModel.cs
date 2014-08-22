using PKB.DomainModel;
using PKB.WPF.Common;
using System.Collections.ObjectModel;

namespace PKB.WPF.ViewModels
{
    public class SectionViewModel : ViewModelBase
    {
        private bool _isInEditMode;

        public SectionViewModel(Section model)
        {
            Model = model;

            Subsections = new SyncViewModelCollection<SectionViewModel, Section>(
                vm => vm.Model,
                m => new SectionViewModel(m),
                model.Subsections)
            {
                ItemAddedHandler = args => model.InsertSubsection(args.Index, args.Item),
                ItemRemovedHandler = args => model.RemoveSubsectionAt(args.Index)
            };

            Topics = new SyncViewModelCollection<TopicViewModel, Topic>(
                vm => vm.Model,
                m => new TopicViewModel(this, m),
                model.Topics)
            {
                ItemAddedHandler = args => model.InsertTopic(args.Index, args.Item),
                ItemRemovedHandler = args => model.RemoveTopicAt(args.Index)
            };
        }

        public SectionViewModel()
            : this(new Section(""))
        {
        }

        public Section Model { get; private set; }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (value == Model.Name) return;
                Model.Name = value;
                OnPropertyChanged();
            }
        }

        public bool IsInEditMode
        {
            get { return _isInEditMode; }
            set
            {
                if (value.Equals(_isInEditMode)) return;
                _isInEditMode = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SectionViewModel> Subsections { get; private set; }

        public ObservableCollection<TopicViewModel> Topics { get; private set; }
    }
}