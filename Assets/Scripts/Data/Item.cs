namespace Assets.Scripts.Data
{
    public abstract class Item
    {
        public string DisplayName;
        public string Image = "Default";
        public string Description;
        public long Mass;
        public long Volume;
        public long Price;
        public long TechLevel;
    }
}