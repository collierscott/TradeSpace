using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts
{
    public class Profile
    {
        public ProtectedValue Credits;
        public MemoShip Ship;
        public List<MemoShop> Shops;
        public List<MemoAsteroid> Asteroids;
        public ProtectedValue InitShopsTime;

        private static Profile _instance;
        private const string ProfileKey = "ST";

        public static Profile Instance
        {
            get
            {
                if (_instance == null)
                {
                    Load();
                }

                return _instance;
            }
        }

        private Profile()
        {
        }

        public static void Load() // TODO: Make private
        {
            GameLog.Write("Loading profile...");

            PlayerPrefs.DeleteAll(); // TODO: WARNING!
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(ProfileKey))
            {
                var profile = PlayerPrefs.GetString(ProfileKey);

                GameLog.Write("Serialized profile: {0}", profile);

                _instance = Serializer.Deserialize<Profile>(profile);
            }
            else
            {
                _instance = new Profile
                {
                    Credits = new ProtectedValue(2000),
                    InitShopsTime = DateTime.UtcNow.Encrypt(),
                    Ship = new MemoShip { Id = ShipId.Rhino },
                    Shops = new List<MemoShop>(),
                    Asteroids = new List<MemoAsteroid>()
                };
                _instance.Ship.Route = new List<RouteNode> { Env.Systems[Env.SystemNames.Andromeda]["Fobos"].ToRouteNode() };
                _instance.Ship.Goods = new List<MemoGoods>
                {
                    new MemoGoods { Id = GoodsId.Water, Quantity = 10.Encrypt() },
                    new MemoGoods { Id = GoodsId.Fish, Quantity = 5.Encrypt() }
                };
                _instance.Ship.Equipment = new List<MemoEquipment>
                {
                    new MemoEquipment { Id = EquipmentId.Armor, Quantity = 5.Encrypt() }
                };
                _instance.Ship.InstalledEquipment = new List<MemoInstalledEquipment>
                {
                    new MemoInstalledEquipment { Id = EquipmentId.EngineReactive, Quantity = 1.Encrypt(), Index = 0 },
                    new MemoInstalledEquipment { Id = EquipmentId.Armor, Quantity = 1.Encrypt(), Index = 1 }
                };
            }
        }

        public void Save()
        {
            GameLog.Write("Saving profile...");

            var profile = Serializer.Serialize(this);

            GameLog.Write("Serialized profile: {0}", profile);

            PlayerPrefs.SetString(ProfileKey, profile);
            PlayerPrefs.Save();
        }
    }
}