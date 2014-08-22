using System;

namespace PKB.WPF.Common
{
    public class ItemdRemovedArgs<TItem> 
    {
        public readonly int Index;
        public readonly TItem Item;

        public ItemdRemovedArgs(int index, TItem item)
        {
            Index = index;
            Item = item;
        }
    }
}