using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using PKB.WPF.Common;

namespace PKB.WPF.Common
{
    public class SyncViewModelCollection<TViewModel, TModel> : ObservableCollection<TViewModel>
    {

        private Func<TViewModel, TModel> GetModelFrom { get; set; }

        public SyncViewModelCollection(Func<TViewModel, TModel> getModel, IEnumerable<TViewModel> viewModels)
            : base(viewModels)
        {
            GetModelFrom = getModel;

            ItemAddedHandler = args => { throw new InvalidOperationException(); };
            ItemRemovedHandler = args => { throw new InvalidOperationException(); };
            ItemsClearedHandler = () => { throw new InvalidOperationException(); };

            ItemMovedHandler = args =>
            {
                ItemRemovedHandler(new ItemdRemovedArgs<TModel>(args.OldIndex, args.Item));
                ItemAddedHandler(new ItemdAddedArgs<TModel>(args.NewIndex, args.Item));
            };

            ItemReplacedHandler = args =>
            {
                ItemRemovedHandler(new ItemdRemovedArgs<TModel>(args.Index, args.OldItem));
                ItemAddedHandler(new ItemdAddedArgs<TModel>(args.Index, args.NewItem));
            };
        }

        public SyncViewModelCollection(Func<TViewModel, TModel> getModel)
            : this(getModel, Enumerable.Empty<TViewModel>())
        {
        }

        public SyncViewModelCollection(
            Func<TViewModel, TModel> getModel,
            Func<TModel, TViewModel> createViewModel,
            IEnumerable<TModel> models)
            : this(getModel, models.Select(createViewModel))
        {
        }

        public Action<ItemdAddedArgs<TModel>> ItemAddedHandler { get; set; }

        public Action<ItemdRemovedArgs<TModel>> ItemRemovedHandler { get; set; }

        public Action<ItemdMovedArgs<TModel>> ItemMovedHandler { get; set; }

        public Action<ItemdReplacedArgs<TModel>> ItemReplacedHandler { get; set; }

        public Action ItemsClearedHandler { get; set; }


        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (int i = 0; i < e.NewItems.Count; i++)
                        OnItemAdded(i + e.NewStartingIndex, (TViewModel)e.NewItems[i]);
                    break;

                case NotifyCollectionChangedAction.Move:
                    for (int i = 0; i < e.NewItems.Count; i++)
                        OnItemMoved(i + e.OldStartingIndex, i + e.NewStartingIndex, (TViewModel)e.NewItems[i]);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    for (int i = 0; i < e.OldItems.Count; i++)
                        OnItemRemoved(e.OldStartingIndex, (TViewModel)e.OldItems[i]);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    for (int i = 0; i < e.OldItems.Count; i++)
                        OnItemReplaced(i + e.OldStartingIndex, (TViewModel)e.OldItems[i], (TViewModel)e.NewItems[i]);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    OnItemsCleared();
                    if (e.NewItems != null)
                        for (int i = 0; i < e.NewItems.Count; i++)
                            OnItemAdded(i, (TViewModel)e.NewItems[i]);
                    break;
            }

            base.OnCollectionChanged(e);
        }

        private void OnItemAdded(int index, TViewModel item)
        {
            ItemAddedHandler(new ItemdAddedArgs<TModel>(index, GetModelFrom(item)));
        }

        private void OnItemMoved(int oldIndex, int newIndex, TViewModel item)
        {
            ItemMovedHandler(new ItemdMovedArgs<TModel>(oldIndex, newIndex, GetModelFrom(item)));
        }

        private void OnItemRemoved(int index, TViewModel item)
        {
            ItemRemovedHandler(new ItemdRemovedArgs<TModel>(index, GetModelFrom(item)));
        }

        private void OnItemReplaced(int index, TViewModel oldItem, TViewModel newItem)
        {
            ItemReplacedHandler(new ItemdReplacedArgs<TModel>(index, GetModelFrom(oldItem), GetModelFrom(newItem)));
        }

        private void OnItemsCleared()
        {
            ItemsClearedHandler();
        }
    }
}