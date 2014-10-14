﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Environment;

namespace Assets.Scripts.Views
{
    public class GalaxyView : ViewBase, IScreenView
    {
        public CargoView CargoView;
        public ShipsView ShipsView;
        public RouteView RouteView;
        public UISprite Background;

        protected override void Initialize()
        {
            var pathLines = new List<PathLine>();

            foreach (var location in Env.Galaxy.Values)
            {
                PrefabsHelper.InstantiateSystem(Panel).GetComponent<SystemButton>().Initialize(location);

                foreach (var system in Env.Routes[location.System].Keys)
                {
                    if (pathLines.Any(i => i.Source == system && i.Destination == location.System))
                    {
                        continue;
                    }

                    GetComponent<NativeRenderer>().DrawHyperLine(Panel, location.Position, Env.Galaxy[system].Position, location.Color.SetAlpha(0.1f), Env.Galaxy[system].Color.SetAlpha(0.1f));
                }
            }

            Background.enabled = true;
            CargoView.Open();
            ShipsView.Open();
            RouteView.Open();
        }

        protected override void Cleanup()
        {
            Panel.Clean();
            Background.enabled = false;
            CargoView.Close();
            ShipsView.Close();
            RouteView.Close();
        }
    }
}