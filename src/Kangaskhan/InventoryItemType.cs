using System;
using System.Collections.Generic;

namespace Kangaskhan
{
    public class InventoryItemType : DataStructures.FactorHolder<InventoryItemTypeFactor>
    {
        public readonly string Name;
        public InventoryItemType(string name) : base(null)
        {
            this.Name = name;
        }
        public InventoryItemType(string name, ICollection<InventoryItemTypeFactor> factors) : base(factors)
        {
            this.Name = name;
        }
    }

    public sealed class InventoryItemTypeNotCompatibleException : Exception
    {
        public InventoryItemTypeNotCompatibleException() : base() { }
    }
}