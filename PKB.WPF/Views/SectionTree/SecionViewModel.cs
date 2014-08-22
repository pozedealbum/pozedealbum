using System;
using System.Collections.ObjectModel;
using System.Linq;
using PKB.DomainModel;
using PKB.Utility;
using PKB.WPF.Common;

namespace PKB.WPF.Views.SectionTree
{
    public class SectionViewModel : ViewModelBase
    {
        private readonly Section _model;

        public SectionViewModel()
            : this("")
        {

        }

        public SectionViewModel(string name)
            : this(new Section(name))
        {

        }

        public SectionViewModel(Section model)
        {
            _model = model;
            Subsections = new SyncChangesObservableCollection<SectionViewModel>(
                model.Subsections.Select(x => new SectionViewModel(x) { Parent = this.ToMaybe() }))
            {
                OnInsertingItem = args =>
                {
                    if (args.Item.Parent.HasValue)
                        throw new InvalidOperationException();

                    model.InsertSubsection(args.Index, args.Item._model);
                    args.Item.Parent = this.ToMaybe();
                },

                OnRemovingItem = args =>
                {
                    if (args.Item.Parent.Value != this)
                        throw new InvalidOperationException();

                    model.RemoveSubsectionAt(args.Index);
                    args.Item.Parent = Maybe<SectionViewModel>.Nothing;
                }
            };
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                value = value.Trim();
                if (value == string.Empty) return;
                if (value == _model.Name) return;

                _model.Name = value;
                OnPropertyChanged();
            }
        }


        public Maybe<SectionViewModel> Parent { get; private set; }

        public ObservableCollection<SectionViewModel> Subsections { get; private set; }

    }
}