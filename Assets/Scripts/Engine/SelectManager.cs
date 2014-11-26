using Assets.Scripts.Behaviour;
using Assets.Scripts.Data;
using Assets.Scripts.Views;

namespace Assets.Scripts.Engine
{
    public class SelectManager : Script
    {
        public static string System { get; private set; }
        public static Location Location { get; private set; }

        public static ShipBehaviour Ship
        {
            get { return ShipsView.Ships[Profile.Instance.SelectedShip.Int]; }
        }

        public static void SelectShip(int index)
        {
            //Debug.Log("Select ship: " + index);
            Profile.Instance.SelectedShip = index;
            FindObjectOfType<IngameMenu>().Refresh();
            FindObjectOfType<CargoView>().Refresh();
            FindObjectOfType<RouteView>().Refresh();
        }

        public static void SelectSystem(string system)
        {
            //Debug.Log("Select system: " + system); 
            System = system;
            FindObjectOfType<IngameMenu>().Reset();
        }

        public static void SelectLocation(Location location)
        {
            //Debug.Log("Select location: " + location.Name); 
            Location = location;
            FindObjectOfType<IngameMenu>().Reset();
        }
    }
}