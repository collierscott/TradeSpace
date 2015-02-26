using System;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using SimpleJSON;

namespace Assets.Scripts
{
    public partial class Profile
    {
        public JSONClass ToJson()
        {
            var json = new JSONClass();
            var ships = new JSONClass();
            var shops = new JSONClass();
            var warehouses = new JSONClass();
            var asteroids = new JSONClass();

            foreach (var ship in Ships)
            {
                ships.Add(ship.Key, ship.Value.ToJson());
            }

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
                asteroids.Add(asteroid.Key, asteroid.Value.ToJson());
            }

            json.Add("Credits", Credits.ToJson());
            json.Add("SelectedShip", SelectedShip.ToJson());
            json.Add("InitShopsTime", InitShopsTime.ToJson());

            json.Add("Ships", ships);
            json.Add("Shops", shops);
            json.Add("Warehouses", warehouses);
            json.Add("Asteroids", asteroids);

            return json;
        }

        public static Profile FromJson(JSONNode json)
        {
            var profile = new Profile
            {
                Credits = ProtectedValue.FromJson(json["Credits"]),
                SelectedShip = ProtectedValue.FromJson(json["SelectedShip"]),
                InitShopsTime = ProtectedValue.FromJson(json["InitShopsTime"])
            };

            foreach (var ship in json["Ships"].AsObject.Keys)
            {
                profile.Ships.Add(ship, MemoShip.FromJson(json["Ships"][ship]));
            }

            foreach (var location in json["Shops"].AsObject.Keys)
            {
                profile.Shops.Add(location, MemoShop.FromJson(json["Shops"][location]));
            }

            foreach (var location in json["Warehouses"].AsObject.Keys)
            {
                profile.Warehouses.Add(location, MemoWarehouse.FromJson(json["Warehouses"][location]));
            }

            foreach (var location in json["Asteroids"].AsObject.Keys)
            {
                profile.Asteroids.Add(location, MemoAsteroid.FromJson(json["Asteroids"][location]));
            }

            if (profile.Ships.Count == 0)
            {
                throw new Exception("profile.Ships.Count == 0");
            }

            return profile;
        }
    }
}