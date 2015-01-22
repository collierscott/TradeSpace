using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Environment.Systems;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public static partial class Env
    {
        public static Dictionary<string, SpaceSystem> Galaxy = new Dictionary<string, SpaceSystem>();
        public static Dictionary<string, Dictionary<string, Location>> Systems = new Dictionary<string, Dictionary<string, Location>>();
        public static Dictionary<string, Dictionary<string, long>> Routes = new Dictionary<string, Dictionary<string, long>>();

        public static class SystemNames
        {
            public const string Alpha = "Alpha";
                public const string Rotos = "Rotos";
                    public const string Amber = "Amber";
                    public const string Union = "Union";
            public const string Beta = "Beta";
            public const string Gamma = "Gamma";
            public const string Delta = "Delta";
            public const string Epsilon = "Epsilon";
            public const string Zeta = "Zeta";
            public const string Eta = "Eta";
            public const string Theta = "Theta";
                public const string Oreon = "Oreon";
                    public const string Phoenix = "Phoenix";
                        public const string Unity = "Unity";
                        public const string Falcon = "Falcon";
        }

        public static void Initialize()
        {
            AddSystem(SpaceSystems.Alpha);
                AddSystem(SpaceSystems.Rotos);
                    AddSystem(SpaceSystems.Union);
                    AddSystem(SpaceSystems.Amber);
            AddSystem(SpaceSystems.Beta);
            AddSystem(SpaceSystems.Gamma);
            AddSystem(SpaceSystems.Delta);
            AddSystem(SpaceSystems.Epsilon);
            AddSystem(SpaceSystems.Zeta);
            AddSystem(SpaceSystems.Eta);
            AddSystem(SpaceSystems.Theta);
                AddSystem(SpaceSystems.Oreon);
                    AddSystem(SpaceSystems.Phoenix);
                        AddSystem(SpaceSystems.Unity);
                        AddSystem(SpaceSystems.Falcon);

            CalcRoutes();
        }

        private static void AddSystem(SpaceSystem system)
        {
            system.System = system.Name;
            system.Image = "SM01";

            foreach (var child in system.Locations)
            {
                child.System = system.Name;
            }

            Galaxy.Add(system.Name, system);

            try
            {
                Systems.Add(system.Name, system.Locations.ToDictionary(i => i.Name));
            }
            catch (Exception)
            {
                Debug.Log(string.Format("Possible name duplicates found in system {0}: {1}", system,
                    string.Join(", ", system.Locations.Select(i => i.Name).GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key).ToArray())));
                throw;
            }
        }

        private static void CalcRoutes()
        {
            foreach (var system in Galaxy)
            {
                var gates = system.Value.Locations.OfType<Gates>();

                Routes.Add(system.Key, gates.ToDictionary(gate => gate.ConnectedSystem,
                    gate => (long)Vector2.Distance(Galaxy[system.Key].Position, Galaxy[gate.ConnectedSystem].Position)));
            }
        }
    }
}