using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Engine
{
    public class ActionManager : Script
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseScreen();
            }
        }

        public void CloseScreen()
        {
            if (BaseView.Current == null) return;

            if (BaseView.Current is SystemView)
            {
                GetComponent<GalaxyView>().Open();
            }
            else if (BaseView.Current is PlanetView || BaseView.Current is StationView || BaseView.Current is AsteroidView)
            {
                GetComponent<SystemView>().Open();
            }
            else if (BaseView.Current is ShopView
                || BaseView.Current is EquipmentShopView || BaseView.Current is HangarView
                || BaseView.Current is StorageView || BaseView.Current is EquipmentStorageView
                || BaseView.Current is ShipShopView)
            {
                BaseView.Previous.Open();
            }
        }

        public void TraceRoute(Location location)
        {
            SelectManager.Ship.BuildTrace(location);
            GetComponent<RouteView>().Refresh();
            GetComponent<IngameMenu>().Reset();
        }

        public void MoveShip(Location location)
        {
            SelectManager.Ship.Move(location);
            GetComponent<IngameMenu>().Reset();
        }

        public void OpenView(string view)
        {
            ((BaseView) GetComponent(view)).Open();
        }

        public void Open()
        {
            if (BaseView.Current is GalaxyView)
            {
                GetComponent<SystemView>().Open();
            }
            else if (BaseView.Current is SystemView)
            {
                if (SelectManager.Location is Planet)
                {
                    GetComponent<PlanetView>().Open();
                }
                else if (SelectManager.Location is Station)
                {
                    GetComponent<StationView>().Open();
                }
                else if (SelectManager.Location is Asteroid)
                {
                    GetComponent<AsteroidView>().Open();
                }
            }
        }

        public void ShowInfo(Location location)
        {
            Debug.Log(string.Format("Welcome to: {0}! The info dialog is not implemented yet", location.Name));

            var planet = location as Planet;

            if (planet != null)
            {
                Debug.Log(string.Format("Type={0}, TechLevel={1}, ProductionLevel={2}, ExportType={3}, ImportType={4}",
                    planet.Type,
                    planet.TechLevel,
                    planet.ProductionLevel,
                    string.Join(", ", planet.ExportType.Select(i => i.ToString()).ToArray()),
                    planet.ImportType));
            }
        }

        public void ShowInfo(string title, string message)
        {
            GetComponent<DialogManager>().Show(title, message);
        }
    }
}