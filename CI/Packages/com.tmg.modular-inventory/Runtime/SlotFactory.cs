namespace TMG.ModularInventory
{
    public class SlotFactory : ISlotFactory
    {
        public ISlot CreateSlot(IItem item)
        {
            return new Slot(item);
        }
    }
}