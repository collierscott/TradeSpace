﻿using System;
using UnityEngine;

namespace Assets.Scripts
{
	public static class PrefabsHelper
	{
        public static GameObject InstantiateSystem(Transform parent)
        {
            return Instantiate("SystemButton", parent);
        }

        public static GameObject InstantiateStar(Transform parent)
	    {
            return Instantiate("StarButton", parent);
	    }

        public static GameObject InstantiateGates(Transform parent)
        {
            return Instantiate("GatesButton", parent);
        }

        public static GameObject InstantiatePlanet(Transform parent)
        {
            return Instantiate("PlanetButton", parent);
        }

        public static GameObject InstantiateStation(Transform parent)
        {
            return Instantiate("StationButton", parent);
        }

        public static GameObject InstantiateAsteroid(Transform parent)
        {
            return Instantiate("AsteroidButton", parent);
        }
        public static GameObject InstantiateLode(Transform parent)
        {
            return Instantiate("LodeButton", parent);
        }

        public static GameObject InstantiateShip(Transform parent)
        {
            return Instantiate("Ship", parent);
        }

        public static GameObject InstantiateGoodsButton(Transform parent)
        {
            return Instantiate("GoodsButton", parent);
        }

        public static GameObject InstantiateInstalledEquipmentButton(Transform parent)
        {
            return Instantiate("InstalledEquipmentButton", parent);
        }

        public static GameObject InstantiateEquipmentButton(Transform parent)
        {
            return Instantiate("EquipmentButton", parent);
        }

        public static GameObject InstantiateShipItemButton(Transform parent)
        {
            return Instantiate("ShipItemButton", parent);
        }

        public static GameObject InstantiateEquipmentSlotButton(Transform parent)
        {
            return Instantiate("EquipmentSlotButton", parent);
        }

        public static GameObject InstantiateRouteFirefly(Transform parent)
        {
            return Instantiate("RouteFirefly", parent);
        }

        public static GameObject InstantiateShipButton(Transform parent)
        {
            return Instantiate("ShipButton", parent);
        }

        private static GameObject Instantiate(string name, Transform parent)
        {
            try
            {
                var instance = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("Prefabs/" + name, typeof(GameObject)));

                instance.name = name;
                instance.transform.parent = parent;
                instance.transform.localScale = Vector3.one;

                return instance;
            }
            catch
            {
                throw new Exception("Prefab not found: " + name);
            }
        }
	}
}