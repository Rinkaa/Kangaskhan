using System;
using System.Collections.Generic;
using System.Linq;
namespace Kangaskhan.Applications.Storages
{
    public class ItemListStorage : InventoryStorage<ItemStorageEntry>
    {
        public readonly int ItemCountLimit;
        public int ItemCount => this.storageList.Count;

        public ItemListStorage() : base()
        {
            this.ItemCountLimit = int.MaxValue;
        }
        public ItemListStorage(int itemCountLimit) : base(itemCountLimit)
        {
            this.ItemCountLimit = itemCountLimit;
        }

        public override bool StoreItem(InventoryItem item)
        {
            if (this.ItemCount < this.ItemCountLimit)
            {
                this.storageList.Add(new ItemStorageEntry(item));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Either store all of the items, or store none of them if this storage is not capacious.
        /// </summary>
        /// <returns>True if stored all of the items.</returns>
        public bool StoreRangeWholly(ICollection<InventoryItem> items)
        {
            if (this.ItemCount + items.Count <= this.ItemCountLimit)
            {
                this.storageList.AddRange(from item in items
                                          select new ItemStorageEntry(item));
                return true;
            }
            else
            {
                return false;
            }
        }

        public override InventoryItem TakeItem(InventoryItemType itemType)
        {
            var storageList = this.storageList;
            for (int i = storageList.Count; i >= 0; i--)
            {
                var listItem = storageList[i];
                if (listItem.ItemType == itemType)
                {
                    this.storageList.RemoveAt(i);
                    return listItem.Item;
                }
            }
            return null;
        }
    }
}