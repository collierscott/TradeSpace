using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class Planet : BaseScreen
    {
        public UITexture Image;
        public UITexture Background;

        protected override void Initialize()
        {
            Image.mainTexture = Resources.Load<Texture2D>("Images/Planets/" + SelectManager.Location.Image);
            Background.mainTexture = Resources.Load<Texture2D>("Images/LocationBackgrounds/" + SelectManager.Location.System);
            GetComponent<ShipSelect>().Refresh();
        }
    }
}