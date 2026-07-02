namespace Save
{
    public interface ISaveable
    {
        void Save(ref SaveSystem.SaveData data);
        void Load(in SaveSystem.SaveData data);
    }
}