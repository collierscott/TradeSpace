using Assets.Scripts.Enums;

namespace Assets.Scripts.Data
{
    public class DrillParams
    {
        public float Power;
        public AsteroidClass Class;
        public DrillType Type;
        public float ReloadSecs = 1;
        /// <summary>
        /// 0-100 per second
        /// </summary>
        public float HeatingRate = 10;
        /// <summary>
        /// 0-100 per second
        /// </summary>
        public float CoolingRate = 20;

        public override string ToString()
        {
            return string.Format("Power:{0}, Class:{1}, Type:{2}, ReloadSecs:{3}, HeatingRate={4}, CoolingRate:{5}", Power, Class, Type, ReloadSecs, HeatingRate, CoolingRate);
        }
    }
}