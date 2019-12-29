using System;
using System.Collections.Generic;
using System.Linq;

namespace Kangaskhan
{
    public class InventoryItem : DataStructures.FactorHolder<InventoryItemFactor>, ICloneable
    {
        public readonly InventoryItemType ItemType;

        public InventoryItem(InventoryItemType itemType) : base(null)
        {
            this.ItemType = itemType;
        }

        public InventoryItem(InventoryItemType itemType, ICollection<InventoryItemFactor> factors) : base(factors)
        {
            this.ItemType = itemType;
        }

        public InventoryItem Clone()
        {
            return new InventoryItem(this.ItemType, this.Factors);
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}