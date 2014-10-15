using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class PlanetButton : LocationButton
    {
        public UITexture Texture;
        public UITexture Effect;

        public override void Initialize(Location location)
        {
            base.Initialize(location);

            Texture.mainTexture = Resources.Load<Texture2D>("Images/Planets/" + location.Image);

            Texture.transform.Rotate(0, 0, RotationHelper.Angle(-location.Position));
            Effect.transform.Rotate(0, 0, RotationHelper.Angle(-location.Position));
        
            Texture.transform.localScale *= location.Scale;
            Effect.transform.localScale *= location.Scale;

            Effect.color = Color.white;
        }
    }
}