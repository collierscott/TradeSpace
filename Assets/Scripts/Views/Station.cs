namespace Assets.Scripts.Views
{
    public class Station : UIScreen
    {
        public UITexture Background;

        protected override void Initialize()
        {
            GetComponent<ShipSelect>().Refresh();
        }
    }
}