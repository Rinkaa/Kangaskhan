namespace Kangaskhan.Applications.Storages
{
    /// <summary>
    /// A single item as an entry in the storage.
    /// </summary>
    public class ItemStorageEntry : InventoryStorageEntry
    {
        public readonly InventoryItem Item;
        public ItemStorageEntry(InventoryItem item) : base(item?.ItemType)
        {
            this.Item = item;
        }
    }
}