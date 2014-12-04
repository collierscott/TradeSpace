namespace Assets.Scripts.Views
{
    public class Station : BaseScreen
    {
        public UITexture Background;

        protected override void Initialize()
        {
            GetComponent<ShipSelect>().Refresh();
        }
    }
}