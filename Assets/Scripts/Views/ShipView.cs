using System.Collections.Generic;
using Assets.Scripts.Behaviour;

namespace Assets.Scripts.Views
{
    public class ShipView : BaseView
    {
        public static readonly Dictionary<string, ShipBehaviour> Ships = new Dictionary<string, ShipBehaviour>();

        protected override void Initialize()
        {
            Ships.Clear();

            foreach (var s in Profile.Instance.Ships)
            {
                var ship = PrefabsHelper.InstantiateShip(Panel).GetComponent<ShipBehaviour>();

                ship.Initialize(s.Value);
                Ships.Add(s.Key, ship);
            }

            GetComponent<ShipSelectView>().Refresh();
        }

        protected override void Cleanup()
        {
            Panel.Clean();
        }
    }
}