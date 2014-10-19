using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class SystemView : ViewBase, IScreenView
    {
        public CargoView CargoView;
        public ShipsView ShipsView;
        public RouteView RouteView;
        public UISprite Background;

        protected override void Initialize()
        {
            var system = SelectManager.System;

            PrefabsHelper.InstantiateStar(Panel).GetComponent<StarButton>().Initialize(Env.Galaxy[system]);

            ShipsView.Open();

            foreach (var location in Env.Systems[system].Select(i => i.Value))
            {
                var orbit = false;

                if (location is Planet)
                {
                    PrefabsHelper.InstantiatePlanet(Panel).GetComponent<LocationButton>().Initialize(location);
                    orbit = true;
                }
                else if (location is Station)
                {
                    PrefabsHelper.InstantiateStation(Panel).GetComponent<LocationButton>().Initialize(location);
                    orbit = true;
                }
                else if (location is Asteroid)
                {
                    PrefabsHelper.InstantiateAsteroid(Panel).GetComponent<LocationButton>().Initialize(location);
                }
                else  if (location is Gates)
                {
                    PrefabsHelper.InstantiateGates(Panel).GetComponent<GatesButton>().Initialize(location);
                }

                if (orbit)
                {
                    GetComponent<NativeRenderer>().DrawOrbit(Panel, location.Position, ColorHelper.GetColor(200, 200, 200, 20));
                }
            }

            GetComponent<TweenMap>().Set(Vector2.zero, 1);
            Background.enabled = true;
            CargoView.Open();
            RouteView.Open();
        }

        protected override void Cleanup()
        {
            Panel.Clean();
            Background.enabled = false;
            CargoView.Close();
            ShipsView.Close();
            RouteView.Close();
            GetComponent<TweenMap>().Set(-Env.Galaxy[SelectManager.System].Position, 1);
        }
    }
}