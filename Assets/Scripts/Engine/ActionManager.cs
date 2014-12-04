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
            if (Base.Current == null) return;

            if (Base.Current is Views.System)
            {
                GetComponent<Galaxy>().Open();
            }
            else if (Base.Current is Planet || Base.Current is Station || Base.Current is Asteroid)
            {
                GetComponent<Views.System>().Open();
            }
            else if (Base.Current is Shop
                || Base.Current is EquipmentShop || Base.Current is Workshop
                || Base.Current is Storage || Base.Current is ShipShop)
            {
                Base.Previous.Open();
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
            ((Base) GetComponent(view)).Open();
        }

        public void Open()
        {
            if (Base.Current is Galaxy)
            {
                GetComponent<Views.System>().Open();
            }
            else if (Base.Current is Views.System && GetComponent<IngameMenu>().OpenButton.Enabled)
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

        public void ShowInfo(string title, string message)
        {
            GetComponent<DialogManager>().Show(title, message);
        }
    }
}