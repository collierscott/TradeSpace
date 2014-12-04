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
            get { return Ships.ShipBehaviours[Profile.Instance.SelectedShip.String]; }
        }

        public static void SelectShip(string uniqName)
        {
            //UnityEngine.Debug.Log("Select ship: " + uniqName);
            Profile.Instance.SelectedShip = uniqName;
            FindObjectOfType<IngameMenu>().Refresh();
            FindObjectOfType<Status>().Refresh();
            FindObjectOfType<Cargo>().Refresh();

            if (Base.Current is Galaxy || Base.Current is Views.System)
            {
                FindObjectOfType<Route>().Refresh();
            }
            else if (Base.Current is BaseShop)
            {
                (Base.Current as BaseShop).Reload();
            }
            else if (Base.Current is Workshop)
            {
                (Base.Current as Workshop).Reload();
            }
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