using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class PathLine : Script
    {
        public string Source;
        public string Destination;

        public void Initialize(Location source, Location destination)
        {
            var sprite = GetComponent<UISprite>();

            sprite.width = (int) Vector2.Distance(source.Position, destination.Position);
            sprite.transform.localPosition = (source.Position + destination.Position) / 2;
            sprite.transform.rotation = Quaternion.Euler(0, 0, -RotationHelper.Angle(destination.Position - source.Position));
            sprite.transform.localScale -= new Vector3(0, 0.8f);
        
            Source = source.System;
            Destination = destination.System;
        }
    }
}