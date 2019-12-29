namespace Kangaskhan.Applications.Storages.AutoPurging
{
    public abstract class ValidatableEntry<TInnerEntry> : InventoryStorageEntry where TInnerEntry : InventoryStorageEntry
    {
        public readonly TInnerEntry InnerEntry;
        public abstract bool IsValid { get; }
        protected ValidatableEntry(TInnerEntry innerEntry) : base(innerEntry?.ItemType) {
            this.InnerEntry = innerEntry;
        }
    }
}