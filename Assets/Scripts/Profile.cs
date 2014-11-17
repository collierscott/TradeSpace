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
    public class Profile
    {
        public ProtectedValue Credits = 0;
        public ProtectedValue SelectedShip = 0;
        public ProtectedValue InitShopsTime = 0;

        public List<MemoShip> Ships;
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

        private Profile()
        {
        }

        public static void Load() // TODO: Make private
        {
            GameLog.Write("Loading profile...");

            //PlayerPrefs.DeleteAll(); // TODO: WARNING!
            //PlayerPrefs.Save();

            //if (PlayerPrefs.HasKey(ProfileKey))
            if (false)
            {
                Debug.Log("Load old profile");
                var profile = PlayerPrefs.GetString(ProfileKey);

                GameLog.Write("Serialized profile: {0}", profile);

                _instance = Serializer.Deserialize<Profile>(profile);
            }
            else
            {
                Debug.Log("Create new profile");

                _instance = new Profile
                {
                    Credits = 2000,
                    InitShopsTime = DateTime.UtcNow,
                    Ships = new List<MemoShip>
                    {
                        new MemoShip { Id = ShipId.Rhino },
                        new MemoShip { Id = ShipId.Rhino }
                    }
                };

                _instance.Ships[0].Route = new List<RouteNode> { Env.Systems[Env.SystemNames.Andromeda]["Fobos"].ToRouteNode() };
                _instance.Ships[0].Goods = new List<MemoGoods>
                {
                    new MemoGoods { Id = GoodsId.Water, Quantity = 10 },
                    new MemoGoods { Id = GoodsId.Fish, Quantity = 5 }
                };
                _instance.Ships[0].Equipment = new List<MemoEquipment>
                {
                    new MemoEquipment { Id = EquipmentId.MassKit100, Quantity = 5 }
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
                    new MemoGoods { Id = GoodsId.Ferrum, Quantity = 10 },
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
            get { return Ships[SelectedShip.Int]; }
        }

        private JSONNode ToJson()
        {
            var json = new JSONClass();
            var shops = new JSONClass();
            var warehouses = new JSONClass();
            var asteroids = new JSONClass();

            foreach (var shop in Shops)
            {
                shops.Add(shop.Key, shop.Value.ToJson());
            }

            foreach (var warehouse in Warehouses)
            {
                warehouses.Add(warehouse.Key, warehouse.Value.ToJson());
            }

            foreach (var asteroid in Asteroids)
            {
                warehouses.Add(asteroid.Key, asteroid.Value.ToJson());
            }

            json.Add("Credits", Credits.ToJson());
            json.Add("SelectedShip", SelectedShip.ToJson());
            json.Add("InitShopsTime", InitShopsTime.ToJson());

            json.Add("Shops", shops);
            json.Add("Warehouses", warehouses);
            json.Add("Asteroids", asteroids);

            return json;
        }

        private static Profile FromJson(JSONNode json)
        {
            var profile = new Profile
            {
                Credits = ProtectedValue.FromJson(json["Credits"]),
                SelectedShip = ProtectedValue.FromJson(json["SelectedShip"]),
                InitShopsTime = ProtectedValue.FromJson(json["InitShopsTime"])
            };

            foreach (var shop in json["Shops"].Childs)
            {
                foreach (var location in shop.AsObject.Keys)
                {
                    profile.Shops.Add(location, MemoShop.FromJson(shop[location]));
                }
            }

            foreach (var warehouse in json["Warehouses"].Childs)
            {
                foreach (var location in warehouse.AsObject.Keys)
                {
                    profile.Warehouses.Add(location, (MemoWarehouse) MemoShop.FromJson(warehouse[location]));
                }
            }

            foreach (var asteroid in json["Asteroids"].Childs)
            {
                foreach (var location in asteroid.AsObject.Keys)
                {
                    profile.Asteroids.Add(location, MemoAsteroid.FromJson(asteroid[location]));
                }
            }

            return profile;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}