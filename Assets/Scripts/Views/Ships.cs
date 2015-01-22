using System.Collections.Generic;
using Assets.Scripts.Behaviour;

namespace Assets.Scripts.Views
{
    public class Ships : UI
    {
        public static readonly Dictionary<string, ShipBehaviour> ShipBehaviours = new Dictionary<string, ShipBehaviour>();

        protected override void Initialize()
        {
            ShipBehaviours.Clear();

            foreach (var s in Profile.Instance.Ships)
            {
                var ship = PrefabsHelper.InstantiateShip(Panel).GetComponent<ShipBehaviour>();

                ship.Initialize(s.Value);
                ShipBehaviours.Add(s.Key, ship);
            }

            GetComponent<ShipSelect>().Refresh();
        }

        protected override void Cleanup()
        {
            Panel.Clear();
        }
    }
}