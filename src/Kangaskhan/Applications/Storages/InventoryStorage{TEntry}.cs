using System;
using System.Collections.Generic;

namespace Kangaskhan.Applications.Storages
{
    /// <summary>
    /// An inventory storage, can be used for implementing player's bag or piles of cards, or for similar cases where you want to set up a collection of game items.
    /// </summary>
    public abstract class InventoryStorage<TEntry> where TEntry : InventoryStorageEntry
    {
        protected readonly List<TEntry> storageList;

        public virtual int EntryCount => this.storageList.Count;

        protected virtual void AddEntry(TEntry entry)
        {
            this.storageList.Add(entry);
        }
        protected virtual void RemoveEntry(TEntry entry)
        {
            this.storageList.Remove(entry);
        }
        public virtual void ForeachEntry(Action<TEntry> action)
        {
            this.storageList.ForEach(action);
        }

        /// <summary>
        /// Store an item into the storage.
        /// </summary>
        /// <returns>Returns `true` if the storing is success.</returns>
        public abstract bool StoreItem(InventoryItem item);
        /// <summary>
        /// Take an item of a certain type out of the storage, and return it.
        /// </summary>
        /// <returns>If an item of this type has been found, returns this item; when there are more than 1 items of this type in the storage, it is unspecified returning which one; when no item of this type has been found, returns null. </returns>
        public abstract InventoryItem TakeItem(InventoryItemType itemType);

        protected InventoryStorage()
        {
            this.storageList = new List<TEntry>();
        }
        protected InventoryStorage(int capacity)
        {
            this.storageList = new List<TEntry>(capacity);
        }
    }
}