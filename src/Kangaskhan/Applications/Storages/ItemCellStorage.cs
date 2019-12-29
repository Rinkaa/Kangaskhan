using System;
using System.Linq;

namespace Kangaskhan.Applications.Storages
{
    public class ItemCellStorage : InventoryStorage<ItemStorageEntry>
    {
        protected bool hasExplicitItemCountLimit;
        public readonly int ItemCountLimit;
        public int ItemCount => this.storageList.Count(x => x != null);

        public ItemCellStorage() : base()
        {
            this.hasExplicitItemCountLimit = false;
            this.ItemCountLimit = int.MaxValue;
            this.fillNullEntries(this.storageList.Capacity);
        }

        public ItemCellStorage(int itemCountLimit) : base(itemCountLimit)
        {
            this.hasExplicitItemCountLimit = true;
            this.ItemCountLimit = itemCountLimit;
            this.fillNullEntries(itemCountLimit);
        }

        private bool tryGetFirstNullEntryIndex(out int resultIndex)
        {
            for (int i = 0; i < this.storageList.Count; i++)
            {
                if (this.storageList[i] == null)
                {
                    resultIndex = i;
                    return true;
                }
            }
            resultIndex = -1;
            return false;
        }
        private void fillNullEntries(int upToCapacity)
        {
            int fillCount = upToCapacity - this.storageList.Count;
            if (fillCount > 0)
            {
                this.storageList.AddRange(Enumerable.Repeat((ItemStorageEntry)null, fillCount));
            }
        }

        public override bool StoreItem(InventoryItem item)
        {
            if (this.tryGetFirstNullEntryIndex(out int nullEntryIndex))
            {
                var newEntry = new ItemStorageEntry(item);
                this.storageList[nullEntryIndex] = newEntry;
                return true;
            }
            else
            {
                if (this.storageList.Count < ItemCountLimit)
                {
                    var newEntry = new ItemStorageEntry(item);
                    this.storageList.Add(newEntry);
                    this.fillNullEntries(Math.Min(this.storageList.Capacity, this.ItemCountLimit));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool StoreItem(InventoryItem item, int index)
        {
            if (index >= 0 && index < this.ItemCountLimit)
            {
                if (index < this.storageList.Count)
                {
                    if (this.storageList[index] != null)
                    {
                        var newEntry = new ItemStorageEntry(item);
                        this.storageList[index] = newEntry;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (this.storageList.Count < ItemCountLimit)
                    {
                        this.storageList.Capacity = index + 1;
                        var newEntry = new ItemStorageEntry(item);
                        this.storageList[index] = newEntry;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("index");
            }
        }

        public override InventoryItem TakeItem(InventoryItemType itemType)
        {
            for (int i = 0; i < this.storageList.Count; i++)
            {
                var entry = storageList[i];
                if (entry != null && entry.ItemType == itemType)
                {
                    var item = entry.Item;
                    storageList[i] = null;
                    return item;
                }
            }
            return null;
        }

        public InventoryItem TakeItem(int index)
        {
            if (index >= 0 && index < this.ItemCountLimit)
            {
                if (index < this.storageList.Count)
                {
                    return this.storageList[index]?.Item;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("index");
            }
        }

    }

}