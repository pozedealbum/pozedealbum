using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PKB.WPF.Common
{
    public class SyncChangesObservableCollection<T> : ObservableCollection<T> where T : class
    {
        public class InsertingItemArgs
        {
            public readonly int Index;
            public readonly T Item;

            public InsertingItemArgs(int index, T item)
            {
                Index = index;
                Item = item;
            }
        }

        public class MovingItemArgs
        {
            public readonly int OldIndex;
            public readonly int NewIndex;
            public readonly T Item;

            public MovingItemArgs(int oldIndex, int newIndex, T item)
            {
                Item = item;
                NewIndex = newIndex;
                OldIndex = oldIndex;
            }
        }

        public class RemovingItemArgs
        {
            public readonly int Index;
            public readonly T Item;

            public RemovingItemArgs(int index, T item)
            {
                Index = index;
                Item = item;
            }
        }

        public class ReplacingItemArgs
        {
            public readonly int Index;
            public readonly T OldItem;
            public readonly T NewItem;

            public ReplacingItemArgs(int index, T oldItem, T newItem)
            {
                NewItem = newItem;
                OldItem = oldItem;
                Index = index;
            }
        }

        public Action<InsertingItemArgs> OnInsertingItem { get; set; }

        public Action<RemovingItemArgs> OnRemovingItem { get; set; }

        public Action<MovingItemArgs> OnMovingIntem { get; set; }

        public Action<ReplacingItemArgs> OnReplacingItem { get; set; }

        public Action OnClearingItems { get; set; }

        public SyncChangesObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            OnInsertingItem = DefaultHandlerForInsert;
            OnRemovingItem = DefaultHandlerForRemove;
            OnClearingItems = DefaultHandlerForClear;
            OnMovingIntem = DefaultHandlerForMove;
            OnReplacingItem = DefaultHandlerForReplace;
        }

        protected override void ClearItems()
        {
            OnClearingItems.Invoke();
            base.ClearItems();
        }

        protected override void InsertItem(int index, T item)
        {
            OnInsertingItem.Invoke(new InsertingItemArgs(index, item));
            base.InsertItem(index, item);
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            OnMovingIntem.Invoke(new MovingItemArgs(oldIndex, newIndex, this[oldIndex]));
            base.MoveItem(oldIndex, newIndex);
        }

        protected override void RemoveItem(int index)
        {
            OnRemovingItem.Invoke(new RemovingItemArgs(index, this[index]));
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            OnReplacingItem.Invoke(new ReplacingItemArgs(index, this[index], item));
            base.SetItem(index, item);
        }

        private void DefaultHandlerForInsert(InsertingItemArgs args)
        {
            throw new InvalidOperationException();
        }

        private void DefaultHandlerForRemove(RemovingItemArgs args)
        {
            throw new InvalidOperationException();
        }

        private void DefaultHandlerForReplace(ReplacingItemArgs args)
        {
            OnRemovingItem(new RemovingItemArgs(args.Index, args.OldItem));
            OnInsertingItem(new InsertingItemArgs(args.Index, args.NewItem));
        }

        private void DefaultHandlerForMove(MovingItemArgs args)
        {
            OnRemovingItem(new RemovingItemArgs(args.OldIndex, args.Item));
            OnInsertingItem(new InsertingItemArgs(args.NewIndex, args.Item));
        }

        private void DefaultHandlerForClear()
        {
            while (Count > 0)
                OnRemovingItem(new RemovingItemArgs(Count - 1, this[Count - 1]));
        }
    }
}