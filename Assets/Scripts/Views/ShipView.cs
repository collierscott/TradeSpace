using System.Collections.Generic;
using Assets.Scripts.Behaviour;

namespace Assets.Scripts.Views
{
    public class ShipView : ViewBase
    {
        public static readonly List<ShipBehaviour> Ships = new List<ShipBehaviour>();
        
        protected override void Initialize()
        {
            Ships.Clear();

            foreach (var s in Profile.Instance.Ships)
            {
                var ship = PrefabsHelper.InstantiateShip(Panel).GetComponent<ShipBehaviour>();

                ship.Initialize(s);
                Ships.Add(ship);
            }

            GetComponent<ShipSelectView>().Refresh();
        }

        protected override void Cleanup()
        {
            Panel.Clean();
        }
    }
}