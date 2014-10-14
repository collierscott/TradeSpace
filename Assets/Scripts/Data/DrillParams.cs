using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class DrillParams
    {
        public float Power;
        public AsteroidClass Class;
        public DrillType Type;
        public float ReloadSeconds = 1;
        public float HeatingRate = 10; // 0-100 per second
        public float CoolingRate = 20; // 0-100 per second
    }
}