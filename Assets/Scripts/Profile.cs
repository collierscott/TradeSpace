using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using Assets.Scripts.Environment;
using SimpleJSON;
using UnityEngine;

namespace Assets.Scripts
{
    public partial class Profile
    {
        public ProtectedValue Credits = 0;
        public ProtectedValue SelectedShip = 0;
        public ProtectedValue InitShopsTime = 0;

        public Dictionary<string, MemoShip> Ships = new Dictionary<string, MemoShip>();
        public Dictionary<string, MemoShop> Shops = new Dictionary<string, MemoShop>();
        public Dictionary<string, MemoWarehouse> Warehouses = new Dictionary<string, MemoWarehouse>();
        public Dictionary<string, MemoAsteroid> Asteroids = new Dictionary<string, MemoAsteroid>();

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

        public MemoShip Ship
        {
            get { return Ships[SelectedShip.String]; }
        }

        private Profile()
        {
        }

        public void Save()
        {
            Debug.Log("Saving profile...");

            var json = ToJson().ToString();

            Debug.Log("Profile json: " + json);

            PlayerPrefs.SetString(ProfileKey, json);
            PlayerPrefs.Save();
        }

        public static void Load()
        {
            Log.Debug("Loading profile...");

            _instance = CreateInstance();

            return;

            if (PlayerPrefs.HasKey(ProfileKey))
            {
                try
                {
                    var json = PlayerPrefs.GetString(ProfileKey);

                    Debug.Log("Profile json: " + json);

                    _instance = FromJson(JSON.Parse(json));
                }
                catch (Exception e)
                {
                    Debug.Log("Error parsing json: " + e);

                    _instance = CreateInstance();
                }
            }
            else
            {
                Log.Debug("Creating new profile...");

                _instance = CreateInstance();
            }
        }

        public static void Reset()
        {
            Debug.Log("Reseting profile...");

            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        private static Profile CreateInstance()
        {
            var instance = new Profile
            {
                Credits = 2000,
                InitShopsTime = DateTime.UtcNow,
                Ships = new Dictionary<string, MemoShip>
                    {
                        { "0", new MemoShip { Id = ShipId.ST500 } },
                        { "1", new MemoShip { Id = ShipId.ST400 } },
                        //{ "2", new MemoShip { Id = ShipId.Rhino } },
                        //{ "3", new MemoShip { Id = ShipId.Rover } }
                    }
            };

            instance.Ships["0"].Route = new List<RouteNode> { Env.Systems[Env.SystemNames.Alpha]["Treunus"].ToRouteNode() };
            instance.Ships["0"].Goods = new List<MemoGoods>
            {
                new MemoGoods { Id = GoodsId.Water, Quantity = 10 },
                new MemoGoods { Id = GoodsId.Fish, Quantity = 5 }
            };
            instance.Ships["0"].Equipment = new List<MemoEquipment>
            {
                new MemoEquipment { Id = EquipmentId.MassKit100, Quantity = 5 }
            };
            instance.Ships["0"].InstalledEquipment = new List<MemoInstalledEquipment>
            {
                new MemoInstalledEquipment { Id = EquipmentId.JetEngine100, Index = 0 },
                new MemoInstalledEquipment { Id = EquipmentId.MassKit100, Index = 1 },
                new MemoInstalledEquipment { Id = EquipmentId.VolumeKit100, Index = 3 },
                new MemoInstalledEquipment { Id = EquipmentId.LaserDrill100, Index = 2 }
            };

            instance.Ships["1"].Route = new List<RouteNode> { Env.Systems[Env.SystemNames.Alpha]["Ketania"].ToRouteNode() };
            instance.Ships["1"].InstalledEquipment = new List<MemoInstalledEquipment>
            {
                new MemoInstalledEquipment { Id = EquipmentId.JetEngine100, Index = 0 }
            };
            instance.Ships["1"].Goods = new List<MemoGoods>
            {
                new MemoGoods { Id = GoodsId.Fish, Quantity = 20 }
            };

            //instance.Ships["2"].Route = new List<RouteNode> { Env.Systems[Env.SystemNames.Alpha]["SS-500"].ToRouteNode() };
            //instance.Ships["2"].InstalledEquipment = new List<MemoInstalledEquipment>
            //{
            //    //new MemoInstalledEquipment {Id = EquipmentId.JetEngine100, Index = 0}
            //};

            //instance.Ships["3"].Route = new List<RouteNode> { Env.Systems[Env.SystemNames.Alpha]["Ketania"].ToRouteNode() };
            //instance.Ships["3"].Goods = new List<MemoGoods>
            //{
            //    new MemoGoods { Id = GoodsId.Ferrum, Quantity = 10 },
            //};
            //instance.Ships["3"].Equipment = new List<MemoEquipment>();
            //instance.Ships["3"].InstalledEquipment = new List<MemoInstalledEquipment>
            //{
            //    new MemoInstalledEquipment { Id = EquipmentId.JetEngine100, Index = 0 },
            //    new MemoInstalledEquipment { Id = EquipmentId.VolumeKit100, Index = 1 }
            //};

            return instance;
        }
    }
}