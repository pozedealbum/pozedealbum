using System;

namespace PKB.WPF.Common
{
    public class ItemdAddedArgs<TItem> 
    {
        public readonly int Index;
        public readonly TItem Item;

        public ItemdAddedArgs(int index, TItem item)
        {
            Index = index;
            Item = item;
        }
    }
}