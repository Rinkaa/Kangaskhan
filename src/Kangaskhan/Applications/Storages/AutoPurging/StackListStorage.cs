using System.Collections.Generic;
using Kangaskhan.Features.Stacking;

namespace Kangaskhan.Applications.Storages.AutoPurging
{
    public class StackListStorage : AutoPurgingStorage<StackStorageEntry>
    {
        public readonly int StackCountLimit;
        public int StackCount => this.storageList.Count;

        public StackListStorage() : base()
        {
            this.StackCountLimit = int.MaxValue;
        }
        public StackListStorage(int capacity) : base(capacity)
        {
            this.StackCountLimit = capacity;
        }

        private IEnumerable<InventoryItemStack> stackesOfExactType(InventoryItemType type)
        {
            int entryCount = this.storageList.Count;
            for (int i = 0; i < entryCount; i++)
            {
                var entry = this.storageList[i];
                if (type == entry.InnerEntry.ItemType)
                {
                    yield return entry.InnerEntry.Stack;
                }
            }
            yield break;
        }
        private ValidatableEntry<StackStorageEntry> makeNewEntry(InventoryItem item)
        {
            return new EmptyCheckedStackStorageEntry(new InventoryItemStack(item, 1));
        }

        protected override bool StoreItemWithoutPurging(InventoryItem item)
        {
            foreach (var stack in stackesOfExactType(item.ItemType))
            {
                if (!stack.IsFull)
                {
                    stack.Stack(item);
                    return true;
                }
            }
            if (this.StackCount < this.StackCountLimit)
            {
                this.storageList.Add(makeNewEntry(item));
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override InventoryItem TakeItemWithoutPurging(InventoryItemType itemType)
        {
            foreach (var stack in stackesOfExactType(itemType))
            {
                if (!stack.IsEmpty)
                {
                    var item = stack.Take();
                    return item;
                }
            }
            return null;
        }
    }
}