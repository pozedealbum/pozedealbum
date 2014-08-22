using System;

namespace PKB.WPF.Common
{
    public class ItemdReplacedArgs<TItem> 
    {
        public readonly int Index;
        public readonly TItem OldItem;
        public readonly TItem NewItem;

        public ItemdReplacedArgs(int index, TItem oldItem, TItem newItem)
        {
            NewItem = newItem;
            OldItem = oldItem;
            Index = index;
        }
    }
}