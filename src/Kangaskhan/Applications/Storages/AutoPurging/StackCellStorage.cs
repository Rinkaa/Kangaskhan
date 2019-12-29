using System;
using System.Collections.Generic;
using System.Linq;
using Kangaskhan.Features.Stacking;

namespace Kangaskhan.Applications.Storages.AutoPurging
{
    public class StackCellStorage : AutoPurgingStorage<StackStorageEntry>
    {
        protected bool hasExplicitStackCountLimit;
        public readonly int StackCountLimit;
        public int StackCount => this.storageList.Count(x => x != null);

        public StackCellStorage() : base()
        {
            this.hasExplicitStackCountLimit = false;
            this.StackCountLimit = int.MaxValue;
            this.fillNullEntries(this.storageList.Capacity);
        }

        public StackCellStorage(int stackCountLimit) : base(stackCountLimit)
        {
            this.hasExplicitStackCountLimit = true;
            this.StackCountLimit = stackCountLimit;
            this.fillNullEntries(stackCountLimit);
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
                this.storageList.AddRange(Enumerable.Repeat((ValidatableEntry<StackStorageEntry>)null, fillCount));
            }
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
            if (this.tryGetFirstNullEntryIndex(out int nullEntryIndex))
            {
                var newEntry = makeNewEntry(item);
                this.storageList[nullEntryIndex] = newEntry;
                return true;
            }
            else
            {
                if (this.storageList.Count < StackCountLimit)
                {
                    var newEntry = makeNewEntry(item);
                    this.storageList.Add(newEntry);
                    this.fillNullEntries(Math.Min(this.storageList.Capacity, this.StackCountLimit));
                    return true;
                }
                else
                {
                    return false;
                }
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