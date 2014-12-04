﻿using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class System : BaseScreen
    {
        public UISprite Background;

        protected override void Initialize()
        {
            var system = SelectManager.System;

            PrefabsHelper.InstantiateStar(Panel).GetComponent<StarButton>().Initialize(Env.Galaxy[system]);

            Open<Ships>();

            foreach (var location in Env.Systems[system].Select(i => i.Value))
            {
                var orbit = false;

                if (location is Data.Planet)
                {
                    PrefabsHelper.InstantiatePlanet(Panel).GetComponent<LocationButton>().Initialize(location);
                    orbit = true;
                }
                else if (location is Data.Station)
                {
                    PrefabsHelper.InstantiateStation(Panel).GetComponent<LocationButton>().Initialize(location);
                    orbit = true;
                }
                else if (location is Data.Asteroid)
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
            Open<Route>();
        }

        protected override void Cleanup()
        {
            Panel.Clear();
            Background.enabled = false;
            Close<Ships>();
            Close<Route>();
            GetComponent<TweenMap>().Set(-Env.Galaxy[SelectManager.System].Position, 1);
        }
    }
}