namespace TMG.ModularInventory
{
    public interface IItem
    {
        string ID { get; }

        T GetModule<T>(string key);
        bool TryGetModule<T>(string key, out T module);
    }
}