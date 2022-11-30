namespace TMG.ModularInventory
{
    public interface ISlotFactory
    {
        ISlot CreateSlot(IItem item);
    }
}