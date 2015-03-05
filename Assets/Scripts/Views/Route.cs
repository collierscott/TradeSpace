using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Common;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class Route : UI
    {
        public float Speed = 2;
        public float Delay = 1;
        public Color FireflyColor = ColorHelper.GetColor(255, 255, 255, 100);

        private int _taskId = 5000;

        protected override void Initialize()
        {
            var locationButtons = FindObjectsOfType<LocationButton>();

            foreach (var locationButton in locationButtons)
            {
                locationButton.ShowArrivalTime = 0;
            }
            
            foreach (var ship in Ships.ShipBehaviours.Values)
            {
                if (ship.State == ShipState.Ready && ship.Trace.Count == 0)
                {
                    continue;
                }

                var route = ship.Trace.Count > 0 ? ship.Trace : ship.Route;
                List<Vector2> polyline;

                if (UIScreen.Current is Galaxy)
                {
                    polyline = route.Select(i => i.System).Distinct().Select(i => Env.Galaxy[i].Position).ToList();

                    if (polyline.Count >= 2)
                    {
                        for (var i = 0; i < polyline.Count - 1; i++)
                        {
                            GetComponent<NativeRenderer>().DrawRouteLine(Panel, polyline[i], polyline[i + 1]);
                        }
                    }
                }
                else if (UIScreen.Current is Systema)
                {
                    polyline = route.Where(i => i.System == SelectManager.System).Select(i => i.Position).ToList();

                    if (polyline.Count == 2)
                    {
                        GetComponent<NativeRenderer>().DrawRouteLine(Panel, polyline[0], polyline[1]);
                    }
                    else if (polyline.Count > 2)
                    {
                        GetComponent<NativeRenderer>().DrawRouteLine(Panel, polyline);
                    }

                    var departure = route.First();
                    var arrival = route.Last();
                    var locationButton = locationButtons.Single(i => i.Name.text == arrival.LocationName);

                    if (ship.State == ShipState.InFlight)
                    {
                        locationButton.ShowArrivalTime = 2;
                        locationButton.ArrivalDateTime = arrival.Time;
                    }
                    else
                    {
                        if (ship.Location.Name == arrival.LocationName)
                        {
                            locationButton.ShowArrivalTime = 0;
                        }
                        else
                        {
                            locationButton.ShowArrivalTime = 1;
                            locationButton.ArrivalTimeSpan = arrival.Time - departure.Time;
                        }
                    }
                }
                else
                {
                    throw new Exception(UIScreen.Current.GetType().Name);
                }

                if (polyline.Count >= 2)
                {
                    StartAnimation(polyline, PrefabsHelper.InstantiateRouteFirefly(Panel));
                }
            }
        }

        public void Refresh()
        {
            Cleanup();
            Initialize();
        }

        protected override void Cleanup()
        {
            Panel.Clear();
            TaskScheduler.Kill(_taskId++);
        }

        private void StartAnimation(IList<Vector2> route, GameObject firefly)
        {
            firefly.transform.localPosition = route[0];
            firefly.GetComponent<UISprite>().color = FireflyColor;

            Animate(route, firefly);
        }

        private void Animate(IList<Vector2> route, GameObject firefly)
        {
            var j = 0;
        
            for (var i = 0; i < route.Count - 1; i++)
            {
                var current = route[i];
                var next = route[i + 1];

                if (Vector2.Distance(current, next) < 100) continue;

                var angle = RotationHelper.Angle(next - current);
                var f = firefly;

                TaskScheduler.CreateTask(() =>
                {
                    f.transform.localPosition = current;
                    f.transform.localRotation = Quaternion.Euler(0, 0, angle);

                    TweenPosition.Begin(f, Speed, next);
                    TweenAlpha.Begin(f, Delay, 1);
                    TweenScale.Begin(f, Delay, new Vector2(1, 8));

                    TaskScheduler.CreateTask(() =>
                    {
                        TweenAlpha.Begin(f, Delay, FireflyColor.a);
                        TweenScale.Begin(f, Delay, Vector2.zero);
                    }, _taskId, Speed - Delay);

                }, _taskId, Speed * j++);
            }

            TaskScheduler.CreateTask(() => StartAnimation(route, firefly), _taskId, Speed * j + 1);
        }
    }
}