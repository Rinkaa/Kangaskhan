using Kangaskhan.Features.Stacking;

namespace Kangaskhan.Applications.Storages.AutoPurging
{
    public class EmptyCheckedStackStorageEntry : ValidatableEntry<StackStorageEntry>
    {
        public override bool IsValid => this.InnerEntry.Stack.IsEmpty;
        public EmptyCheckedStackStorageEntry(InventoryItemStack stack) : base(new StackStorageEntry(stack))
        { }
    }
}