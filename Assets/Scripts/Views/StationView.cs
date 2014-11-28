namespace Assets.Scripts.Views
{
    public class StationView : ViewBase, IScreenView
    {
        public UITexture Background;

        protected override void Initialize()
        {
            GetComponent<ShipSelectView>().Refresh();
        }
    }
}