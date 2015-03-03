using System.Linq;
using Assets.Scripts.Behaviour;
using Assets.Scripts.Engine;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class Asteroid : UIScreen
    {
        public Transform Space;

        protected override void Initialize()
        {
            var equipment = Profile.Instance.Ship.InstalledEquipment.Single(i => Env.EquipmentDatabase[i.Id].Type == EquipmentType.Drill);
            var drill = Env.GetDrillParams(equipment.Id);
            var asteroid = (Data.Asteroid) Env.Systems[SelectManager.Location.System][SelectManager.Location.Name];

            if (Profile.Instance.Asteroids.ContainsKey(asteroid.Name))
            {
                asteroid.Merge(Profile.Instance.Asteroids[asteroid.Name].Extracted);
            }

            for (var i = 0; i < asteroid.Lodes.Count; i++)
            {
                if (asteroid.Lodes[i] != null)
                {
                    PrefabsHelper.InstantiateLode(Space).GetComponent<LodeButton>().Initialize(asteroid.Lodes[i], i, drill);
                }
            }

            Open<Cargo>();
            GetComponent<ShipSelect>().Refresh();
        }

        protected override void Cleanup()
        {
            Close<Cargo>();
            Space.Clear();
        }
    }
}