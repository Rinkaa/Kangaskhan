using Kangaskhan.Features.Stacking;

namespace Kangaskhan.Applications.Storages
{
    /// <summary>
    /// A stack of items as an entry in the storage.
    /// </summary>
    public class StackStorageEntry : InventoryStorageEntry
    {
        public readonly InventoryItemStack Stack;
        public StackStorageEntry(InventoryItemStack stack) : base(stack?.ItemType)
        {
            this.Stack = stack;
        }
    }
}