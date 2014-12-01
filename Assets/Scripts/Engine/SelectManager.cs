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
            get { return ShipView.Ships[Profile.Instance.SelectedShip.String]; }
        }

        public static void SelectShip(string uniqName)
        {
            //Debug.Log("Select ship: " + index);
            Profile.Instance.SelectedShip = uniqName;
            FindObjectOfType<IngameMenu>().Refresh();
            FindObjectOfType<StatusView>().Refresh();
            FindObjectOfType<CargoView>().Refresh();

            if (BaseView.Current is GalaxyView || BaseView.Current is SystemView)
            {
                FindObjectOfType<RouteView>().Refresh();
            }

            if (BaseView.Current is BaseShopView)
            {
                (BaseView.Current as BaseShopView).Reload();
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