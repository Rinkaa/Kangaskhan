using System;

namespace Kangaskhan.Applications.Storages
{
    /// <summary>
    /// An entry for an inventory storage.
    /// Can be used for customizing entries for custom storage implements.
    /// </summary>
    public abstract class InventoryStorageEntry
    {
        public readonly InventoryItemType ItemType;

        /// <summary>
        /// Create an entry for an inventory storage.
        /// </summary>
        /// <param name="item">The item type specified by this entry.</param>
        /// <exception cref="ArgumentNullException">
        /// Throws exception if attempting to create an entry of null item type.
        /// </exception>
        protected InventoryStorageEntry(InventoryItemType itemType)
        {
            if (itemType == null)
            {
                throw new ArgumentNullException();
            }
            this.ItemType = itemType;
        }
    }
}