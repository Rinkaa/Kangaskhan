using System;
using System.Collections.Generic;

namespace Kangaskhan.Features.Stacking
{
    public class StackableTypeFactor : InventoryItemTypeFactor
    {
        public readonly int StackLimit;
        public StackableTypeFactor()
        {
            this.StackLimit = int.MaxValue;
        }
        public StackableTypeFactor(int stackLimit)
        {
            if (stackLimit > 0)
            {
                this.StackLimit = stackLimit;
            }
            else
            {
                throw new ArgumentOutOfRangeException("stackLimit");
            }
        }

        public InventoryItemStack Stack(InventoryItemStack stack, InventoryItem incoming)
        {
            var resultStack = new InventoryItemStack(stack.TypicalItem, stack.Count);
            this.Stack(resultStack, incoming);
            return resultStack;
        }

        public InventoryItemStack StackRange(InventoryItemStack stack, IEnumerable<InventoryItem> incomings)
        {
            var resultStack = new InventoryItemStack(stack.TypicalItem, stack.Count);
            this.StackRangeInPlace(resultStack, incomings);
            return resultStack;
        }

        public virtual void StackInPlace(InventoryItemStack stack, InventoryItem incoming)
        {
            var typicalItem = stack.TypicalItem;
            if (incoming.ItemType == typicalItem.ItemType)
            {
                stack.Count += 1;
            }
            else
            {
                throw new InventoryItemTypeNotCompatibleException();
            }
        }

        public virtual void StackRangeInPlace(InventoryItemStack stack, IEnumerable<InventoryItem> incomings)
        {
            var typicalItem = stack.TypicalItem;
            var addNum = 0;
            foreach (var item in incomings)
            {
                if (item.ItemType == typicalItem.ItemType)
                {
                    addNum++;
                }
                else
                {
                    throw new InventoryItemTypeNotCompatibleException();
                }
            }
            stack.Count += addNum;
        }

    }

    public sealed class InventoryItemNotStackableTypeException : Exception
    {
        public InventoryItemNotStackableTypeException() : base() { }
    }
}