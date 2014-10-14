using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Environment.AmberSystem;
using Assets.Scripts.Environment.AndromedaSystem;
using Assets.Scripts.Environment.OreonSystem;
using Assets.Scripts.Environment.PhoenixSystem;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        public static Dictionary<string, Location> Galaxy = new Dictionary<string, Location>();
        public static Dictionary<string, Dictionary<string, Location>> Systems = new Dictionary<string, Dictionary<string, Location>>();
        public static Dictionary<string, Dictionary<string, long>> Routes = new Dictionary<string, Dictionary<string, long>>();

        public static class SystemNames
        {
            public const string Andromeda = "Andromeda";
            public const string Amber = "Amber";
            public const string Phoenix = "Phoenix";
            public const string Oreon = "Oreon";
        }

        public static void Initialize()
        {
            AddSystem(SystemNames.Andromeda, new Vector2(-100, -100), ColorHelper.GetColor(0, 160, 255, 180),
                Andromeda.Fobos, Andromeda.Ketania, Andromeda.Treunus, Andromeda.Station, Andromeda.GatesToAmber, Andromeda.GatesToOreon, Andromeda.A100200);

            AddSystem(SystemNames.Amber, new Vector2(100, 100), ColorHelper.GetColor(255, 255, 0, 180),
                Amber.Eqsmion, Amber.Skayolara, Amber.Zelos, Amber.GatesToAndromeda, Amber.GatesToPhoenix);

            AddSystem(SystemNames.Phoenix, new Vector2(400, -200), ColorHelper.GetColor(255, 0, 0, 180),
                Phoenix.A200250, Phoenix.GatesToAmber);

            AddSystem(SystemNames.Oreon, new Vector2(-300, 300), ColorHelper.GetColor(177, 34, 190, 180),
                Oreon.Osiris, Oreon.GatesToAndromeda);

            CalcRoutes();
        }

        private static void AddSystem(string system, Vector2 position, Color color, params Location[] locations)
        {
            var galaxy = new Location
            {
                System = system,
                Image = "SM01",
                Position = position,
                Color = color
            };

            foreach (var location in locations)
            {
                location.System = system;
            }

            Galaxy.Add(system, galaxy);

            try
            {
                Systems.Add(system, locations.ToDictionary(i => i.Name));
            }
            catch (Exception)
            {
                Debug.Log(string.Format("Possible name duplicates found in system {0}: {1}", system,
                    string.Join(", ", locations.Select(i => i.Name).GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key).ToArray())));
                throw;
            }
        }

        private static void CalcRoutes()
        {
            foreach (var system in Systems)
            {
                var gates = system.Value.Values.OfType<Gates>();

                Routes.Add(system.Key, gates.ToDictionary(gate => gate.ConnectedSystem,
                    gate => (long)Vector2.Distance(Galaxy[system.Key].Position, Galaxy[gate.ConnectedSystem].Position)));
            }
        }
    }
}