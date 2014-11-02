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
        public List<MemoShip> Ships;
        public int SelectedShip;
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

            //if (PlayerPrefs.HasKey(ProfileKey))
            if(false)
            {
                //var profile = PlayerPrefs.GetString(ProfileKey);

                //GameLog.Write("Serialized profile: {0}", profile);

                //_instance = Serializer.Deserialize<Profile>(profile);
            }
            else
            {
                _instance = new Profile
                {
                    Credits = new ProtectedValue(2000),
                    InitShopsTime = DateTime.UtcNow.Encrypt(),
                    Ships = new List<MemoShip>
                    {
                        new MemoShip { Id = ShipId.Rhino },
                        new MemoShip { Id = ShipId.Rhino }
                    },
                    Shops = new List<MemoShop>(),
                    Asteroids = new List<MemoAsteroid>()
                };

                _instance.Ships[0].Route = new List<RouteNode> { Env.Systems[Env.SystemNames.Andromeda]["Fobos"].ToRouteNode() };
                _instance.Ships[0].Goods = new List<MemoGoods>
                {
                    new MemoGoods { Id = GoodsId.Water, Quantity = 10.Encrypt() },
                    new MemoGoods { Id = GoodsId.Fish, Quantity = 5.Encrypt() }
                };
                _instance.Ships[0].Equipment = new List<MemoEquipment>
                {
                    new MemoEquipment { Id = EquipmentId.MassKit100, Quantity = 5.Encrypt() }
                };
                _instance.Ships[0].InstalledEquipment = new List<MemoInstalledEquipment>
                {
                    new MemoInstalledEquipment { Id = EquipmentId.JetEngine100, Index = 0 },
                    new MemoInstalledEquipment { Id = EquipmentId.MassKit100, Index = 1 },
                    //new MemoInstalledEquipment { Id = EquipmentId.ImpulseDrill100, Index = 2 }
                    new MemoInstalledEquipment { Id = EquipmentId.LaserDrill100, Index = 2 }
                };

                _instance.Ships[1].Route = new List<RouteNode> { Env.Systems[Env.SystemNames.Andromeda]["Ketania"].ToRouteNode() };
                _instance.Ships[1].Goods = new List<MemoGoods>
                {
                    new MemoGoods { Id = GoodsId.Ferrum, Quantity = 10.Encrypt() },
                };
                _instance.Ships[1].Equipment = new List<MemoEquipment>();
                _instance.Ships[1].InstalledEquipment = new List<MemoInstalledEquipment>
                {
                    new MemoInstalledEquipment { Id = EquipmentId.JetEngine100, Index = 0 },
                    new MemoInstalledEquipment { Id = EquipmentId.VolumeKit100, Index = 1 }
                };
            }
        }

        public MemoShip Ship
        {
            get { return Ships[SelectedShip]; }
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