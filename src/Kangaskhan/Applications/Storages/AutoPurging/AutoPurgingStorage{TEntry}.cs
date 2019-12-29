using System;

namespace Kangaskhan.Applications.Storages.AutoPurging
{
    public abstract class AutoPurgingStorage<TEntry> : InventoryStorage<ValidatableEntry<TEntry>> where TEntry : InventoryStorageEntry
    {
        /// <summary>
        /// Remove invalid entries in this storage.
        /// </summary>
        /// <returns>Count of entries removed.</returns>
        protected virtual int Purge()
        {
            int removedCount = 0;
            foreach (var entry in this.storageList)
            {
                if (!entry.IsValid)
                {
                    this.storageList.Remove(entry);
                    removedCount++;
                }
            }
            return removedCount;
        }

        public override int EntryCount
        {
            get
            {
                this.Purge();
                return this.storageList.Count;
            }
        }

        public AutoPurgingStorage() : base() { }
        public AutoPurgingStorage(int capacity) : base(capacity) { }

        protected override void AddEntry(ValidatableEntry<TEntry> entry)
        {
            this.Purge();
            base.AddEntry(entry);
        }
        protected override void RemoveEntry(ValidatableEntry<TEntry> entry)
        {
            this.Purge();
            base.RemoveEntry(entry);
        }

        public override void ForeachEntry(Action<ValidatableEntry<TEntry>> action)
        {
            this.Purge();
            base.ForeachEntry(action);
        }
        public void ForeachEntry(Action<TEntry> action)
        {
            this.Purge();
            base.ForeachEntry(x => action(x.InnerEntry));
        }

        protected abstract bool StoreItemWithoutPurging(InventoryItem item);
        public override bool StoreItem(InventoryItem item)
        {
            this.Purge();
            return this.StoreItemWithoutPurging(item);
        }

        protected abstract InventoryItem TakeItemWithoutPurging(InventoryItemType itemType);
        public override InventoryItem TakeItem(InventoryItemType itemType)
        {
            this.Purge();
            return this.TakeItemWithoutPurging(itemType);
        }
    }
}