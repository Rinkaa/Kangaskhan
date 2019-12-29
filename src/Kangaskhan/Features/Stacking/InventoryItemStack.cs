using System;
using System.Collections.Generic;
using System.Linq;

namespace Kangaskhan.Features.Stacking
{
    public class InventoryItemStack
    {
        public readonly InventoryItemType ItemType;
        private readonly StackableTypeFactor stackableTypeFactor;
        public readonly int StackLimit;

        public InventoryItem TypicalItem { get; protected set; }

        /// <summary>
        /// The number of items inside the stack.
        /// Notice that 0 is an acceptable value, useful for cases where reusing the stacks is an option to reduce allocations for new stacks. 
        /// Notice that for an unstackable item type, you can still make a stack of items of that kind at the count of no more than 1.
        /// </summary>
        /// <value></value>
        public int Count
        {
            get { return count; }
            set
            {
                if (value < 0)
                {
                    throw new InventoryItemStackCountNotEnoughException();
                }
                else if (value > this.StackLimit)
                {
                    throw new InventoryItemStackCountOverLimitException();
                }
                else
                {
                    this.count = value;
                }
            }
        }
        private int count;
        
        public bool IsEmpty => (this.Count == 0);
        public bool IsFull => (this.Count >= this.StackLimit);

        /// <summary>
        /// Make a stack of inventory items. A stack is multiple items holding up together, usually displaying as a single unit in a game.
        /// Notice that for an unstackable item type, you can still make a stack of items of that kind at the count of no more than 1.
        /// </summary>
        /// <param name="typicalItem">The typical item you want this stack to stack as.</param>
        /// <param name="count">The count of items.</param>
        public InventoryItemStack(InventoryItem typicalItem, int count)
        {
            this.ItemType = typicalItem.ItemType;
            this.stackableTypeFactor = this.ItemType.GetFactor<StackableTypeFactor>();
            if (this.stackableTypeFactor == null && count > 1)
            {
                throw new InventoryItemNotStackableTypeException();
            }
            this.StackLimit = this.stackableTypeFactor?.StackLimit ?? count;

            this.TypicalItem = typicalItem.Clone();
            this.Count = count;
        }
        public InventoryItemStack(IEnumerable<InventoryItem> items) : this(items.First(), 1)
        {
            var itemsLeft = items.Skip(1);
            this.stackableTypeFactor.StackRangeInPlace(this, itemsLeft);
        }

        public void Stack(InventoryItem item)
        {
            this.stackableTypeFactor.StackInPlace(this, item);
        }

        public void StackRange(IEnumerable<InventoryItem> items)
        {
            this.stackableTypeFactor.StackRangeInPlace(this, items);
        }

        public InventoryItem Take()
        {
            var typicalItem = this.TypicalItem;
            this.Count--;
            return typicalItem;
        }

    }

    public sealed class InventoryItemStackCountNotEnoughException : Exception
    {
        public InventoryItemStackCountNotEnoughException() : base() { }
    }
    public sealed class InventoryItemStackCountOverLimitException : Exception
    {
        public InventoryItemStackCountOverLimitException() : base() { }
    }
}