using Assets.Scripts.Behaviour;
using Assets.Scripts.Engine;

namespace Assets.Scripts.Views
{
    public class ShipsView : ViewBase
    {
        public ShipBehaviour Ship;

        protected override void Initialize()
        {
            Ship = PrefabsHelper.InstantiateShip(Panel).GetComponent<ShipBehaviour>();
            Ship.Initialize(Profile.Instance.Ship);
            SelectManager.SelectShip(Ship);
        }

        protected override void Cleanup()
        {
            Panel.Clean();
        }
    }
}