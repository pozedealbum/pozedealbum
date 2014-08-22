namespace PKB.WPF.Common
{
    public class ItemdMovedArgs<TItem> 
    {
        public readonly int OldIndex;
        public readonly int NewIndex;
        public readonly TItem Item;

        public ItemdMovedArgs(int oldIndex, int newIndex, TItem item)
        {
            Item = item;
            NewIndex = newIndex;
            OldIndex = oldIndex;
        }
    }
}