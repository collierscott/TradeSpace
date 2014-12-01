namespace Assets.Scripts.Views
{
    public class StationView : BaseScreenView
    {
        public UITexture Background;

        protected override void Initialize()
        {
            GetComponent<ShipSelectView>().Refresh();
        }
    }
}