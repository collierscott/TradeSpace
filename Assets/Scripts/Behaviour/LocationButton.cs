using System;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class LocationButton : Script
    {
        public UILabel Name;
        public UISprite Image;
        public SelectButton Button;
        public UILabel ArrivalTime;

        [HideInInspector] public int ShowArrivalTime;
        [HideInInspector] public TimeSpan ArrivalTimeSpan;
        [HideInInspector] public DateTime ArrivalDateTime;

        public void Update()
        {
            if (ArrivalTime == null) return;

            switch (ShowArrivalTime)
            {
                case 0:
                    ArrivalTime.SetText(null);
                    break;
                case 1:
                    ArrivalTime.SetText(TimespanToString(ArrivalTimeSpan));
                    break;
                case 2:
                    ArrivalTime.SetText(TimespanToString(ArrivalDateTime - DateTime.UtcNow));
                    break;
            }
        }

        public virtual void Initialize(Location location)
        {
            Name.text = location.Name;

            if (Image != null)
            {
                Image.spriteName = location.Image;
            }

            transform.localPosition = location.Position;

            if (location.Pivot != Vector2.right)
            {
                if (location.Pivot == -Vector2.right)
                {
                    var p = Name.transform.localPosition;

                    Name.pivot = UIWidget.Pivot.Right;
                    Name.transform.localPosition = -p;
                }
            }

            Subscribe(location);

            if (location.Name == SelectManager.Ship.Location.Name)
            {
                Button.Pressed = true;
            }
        }
   
        private void Subscribe(Location location)
        {
            Button.Selected += () => SelectManager.SelectLocation(location);
            Button.Confirmed += Find<ActionManager>().Open;
        }
    }
}