using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class ShipBehaviour : Script
    {
        public UISprite Image;
        public UILabel Name;
        public Location Location;

        private MemoShip _ship;

        public ShipState State
        {
            get { return _ship.State; }
        }

        public List<RouteNode> Route
        {
            get { return _ship.Route; }
        }

        public List<RouteNode> Trace
        {
            get { return _ship.Trace; }
        }

        public void Initialize(MemoShip ship)
        {
            _ship = ship;
            Name.text = ship.Id.ToString();
            Image.spriteName = Env.ShipDatabase[_ship.Id].Image;
            Location = CalcLocation();
        }

        public void Update()
        {
            Location = CalcLocation();

            if (_ship.State == ShipState.InFlight && Location.Name != null)
            {
                FindObjectOfType<RouteView>().Close();
                _ship.State = ShipState.Ready;
            }

            if (ViewBase.Current is SystemView && Location.System != SelectManager.System)
            {
                Hide();
            }
            else if (ViewBase.Current is GalaxyView && Location.System != null)
            {
                ShowStatic(Location);
            }
            else
            {
                ShowDynamic(Location);
            }
        }

        public void Move(Location arrival)
        {
            var ship = new PlayerShip(_ship);

            _ship.Trace = null;
            _ship.Route = RouteEngine.FindRoute(_ship.Route.Last(), arrival.ToRouteNode(), ship.Speed);
            _ship.State = ShipState.InFlight;
        }

        public void BuildTrace(Location arrival)
        {
            var ship = new PlayerShip(_ship);

            _ship.Trace = RouteEngine.FindRoute(_ship.Route.Last(), arrival.ToRouteNode(), ship.Speed);
            _ship.State = ShipState.Ready;
        }

        private void ShowStatic(Location location)
        {
            Image.enabled = Name.enabled = true;
            transform.localPosition = Env.Galaxy[location.System].Position;
            Image.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void ShowDynamic(Location location)
        {
            Image.enabled = Name.enabled = true;
            transform.localPosition = location.Position;
            Image.transform.rotation = Quaternion.Euler(0, 0, location.Rotation);
        }

        private void Hide()
        {
            Image.enabled = Name.enabled = false;
        }

        private Location CalcLocation()
        {
            var now = DateTime.UtcNow;
            var arrival = _ship.Route.Last();

            if (now >= arrival.Time)
            {
                return new Location
                {
                    System = arrival.System,
                    Name = arrival.LocationName,
                    Position = arrival.Position,
                    Rotation = 0
                };
            }

            for (var i = 1; i < _ship.Route.Count; i++)
            {
                if (now >= _ship.Route[i].Time) continue;

                var progress = (float)((now - _ship.Route[i - 1].Time).TotalMilliseconds / (_ship.Route[i].Time - _ship.Route[i - 1].Time).TotalMilliseconds);

                return _ship.Route[i - 1].System == _ship.Route[i].System
                    ? new Location
                    {
                        System = _ship.Route[i].System,
                        Name = null,
                        Position = _ship.Route[i - 1].Position + progress * (_ship.Route[i].Position - _ship.Route[i - 1].Position),
                        Rotation = RotationHelper.Angle(_ship.Route[i].Position - _ship.Route[i - 1].Position) // TODO: Speedup
                    }
                    : new Location
                    {
                        System = null,
                        Name = null,
                        Position = Env.Galaxy[_ship.Route[i - 1].System].Position
                                   + progress * (Env.Galaxy[_ship.Route[i].System].Position - Env.Galaxy[_ship.Route[i - 1].System].Position),
                        Rotation = RotationHelper.Angle(Env.Galaxy[_ship.Route[i].System].Position - Env.Galaxy[_ship.Route[i - 1].System].Position) // TODO: Speedup
                    };
            }

            throw new Exception();
        }
    }
}