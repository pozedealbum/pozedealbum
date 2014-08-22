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

        public Action<InsertingItemArgs> SyncInsertItem { get; set; }

        public Action<RemovingItemArgs> SyncRemoveItem { get; set; }

        public Action<MovingItemArgs> SyncMoveIntem { get; set; }

        public Action<ReplacingItemArgs> SyncReplaceItem { get; set; }

        public Action SyncClearItems { get; set; }

        public SyncChangesObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            SyncInsertItem = DefaultHandlerForInsert;
            SyncRemoveItem = DefaultHandlerForRemove;
            SyncClearItems = DefaultHandlerForClear;
            SyncMoveIntem = DefaultHandlerForMove;
            SyncReplaceItem = DefaultHandlerForReplace;
        }

        protected override void ClearItems()
        {
            SyncClearItems.Invoke();
            base.ClearItems();
        }

        protected override void InsertItem(int index, T item)
        {
            SyncInsertItem.Invoke(new InsertingItemArgs(index, item));
            base.InsertItem(index, item);
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            SyncMoveIntem.Invoke(new MovingItemArgs(oldIndex, newIndex, this[oldIndex]));
            base.MoveItem(oldIndex, newIndex);
        }

        protected override void RemoveItem(int index)
        {
            SyncRemoveItem.Invoke(new RemovingItemArgs(index, this[index]));
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            SyncReplaceItem.Invoke(new ReplacingItemArgs(index, this[index], item));
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
            SyncRemoveItem(new RemovingItemArgs(args.Index, args.OldItem));
            SyncInsertItem(new InsertingItemArgs(args.Index, args.NewItem));
        }

        private void DefaultHandlerForMove(MovingItemArgs args)
        {
            SyncRemoveItem(new RemovingItemArgs(args.OldIndex, args.Item));
            SyncInsertItem(new InsertingItemArgs(args.NewIndex, args.Item));
        }

        private void DefaultHandlerForClear()
        {
            while (Count > 0)
                SyncRemoveItem(new RemovingItemArgs(Count - 1, this[Count - 1]));
        }
    }
}