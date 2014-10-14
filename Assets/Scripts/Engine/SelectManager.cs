using Assets.Scripts.Behaviour;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Views;

namespace Assets.Scripts.Engine
{
    public class SelectManager : Script
    {
        public static string System { get; private set; }
        public static Location Location { get; private set; }
        public static ShipBehaviour Ship { get; private set; }

        public static void SelectShip(ShipBehaviour ship)
        {
            GameLog.Write("Select ship: {0}", ship.Name.text); 
            Ship = ship;
        }

        public static void SelectSystem(string system)
        {
            GameLog.Write("Select system: {0}", system); 
            System = system;
            FindObjectOfType<IngameMenu>().Refresh();
        }

        public static void SelectLocation(Location location)
        {
            GameLog.Write("Select location: {0}", location.Name); 
            Location = location;
            FindObjectOfType<IngameMenu>().Refresh();
        }
    }
}