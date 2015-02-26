using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Views;
using UnityEngine;
using Asteroid = Assets.Scripts.Views.Asteroid;
using Planet = Assets.Scripts.Views.Planet;
using Station = Assets.Scripts.Views.Station;

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
            if (UIScreen.Current == null) return;

            if (UIScreen.Current is Systema)
            {
                GetComponent<Galaxy>().Open();
            }
            else if (UIScreen.Current is Planet || UIScreen.Current is Station || UIScreen.Current is Asteroid)
            {
                GetComponent<Systema>().Open();
            }
            else if (UIScreen.Current is Shop
                || UIScreen.Current is EquipmentShop || UIScreen.Current is Workshop
                || UIScreen.Current is Storage || UIScreen.Current is ShipShop)
            {
                UIScreen.Prev.Open();
            }
        }

        public void TraceRoute(Location location)
        {
            SelectManager.Ship.BuildTrace(location);
            GetComponent<Route>().Refresh();
            GetComponent<IngameMenu>().Reset();
        }

        public void MoveShip(Location location)
        {
            SelectManager.Ship.Move(location);
            GetComponent<IngameMenu>().Reset();
        }

        public void OpenView(string view)
        {
            ((UI) GetComponent(view)).Open();
        }

        public void Open()
        {
            if (UIScreen.Current is Galaxy)
            {
                GetComponent<Systema>().Open();
            }
            else if (UIScreen.Current is Systema && GetComponent<IngameMenu>().OpenButton.Enabled)
            {
                if (SelectManager.Location is Data.Planet)
                {
                    GetComponent<Planet>().Open();
                }
                else if (SelectManager.Location is Data.Station)
                {
                    GetComponent<Station>().Open();
                }
                else if (SelectManager.Location is Data.Asteroid)
                {
                    GetComponent<Asteroid>().Open();
                }
            }
        }

        public void ShowInfo(Location location)
        {
            Debug.Log(string.Format("Welcome to: {0}! The info dialog is not implemented yet", location.Name));

            var planet = location as Data.Planet;

            if (planet != null)
            {
                Debug.Log(string.Format("Type={0}, TechLevel={1}, ProductionLevel={2}, ExportType={3}, ImportType={4}",
                    planet.Type,
                    planet.TechLevel,
                    planet.ProductionLevel,
                    string.Join(", ", planet.ExportType.Select(i => i.ToString()).ToArray()),
                    planet.Import));
            }
        }

        public void ShowInfo(string subject, string message)
        {
            GetComponent<Dialog>().Open(subject, message);
        }
    }
}