using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Alpha = new SpaceSystem
        {
            Name = Env.SystemNames.Alpha,
            Position = new Vector2(280, 620),
            Color = ColorHelper.GetColor("#0099FF", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Theta },
                new Gates { ConnectedSystem = Env.SystemNames.Beta },
                new Gates { ConnectedSystem = Env.SystemNames.Rotos },
                new Planet
                {
                    Name = "Fobos",
                    Position = new Vector2(-40, 360),
                    Image = "21600",
                    Color = ColorHelper.GetColor(255, 255, 74),
                    Description = "Description",
                    Interference = 0,
                    Radiation = 0,
                    ImportRate = 1.2f,
                    Export = new List<ShopGoods>
                    {
                        new ShopGoods {Id = GoodsId.Smartphone, Min = 20, Max = 80, Availability = 1}
                    },
                    Import = new List<GoodsType> {GoodsType.Food}
                },
                new Planet
                {
                    Name = "Ketania",
                    Position = new Vector2(100, -300),
                    Image = "6574",
                    Description = "Description",
                    Interference = 0,
                    Radiation = 0,
                    Export = new List<ShopGoods>
                    {
                        new ShopGoods {Id = GoodsId.Water, Min = 100, Max = 200, Availability = 1},
                        new ShopGoods {Id = GoodsId.Fish, Min = 40, Max = 100, Availability = 1},
                    },
                    Import = new List<GoodsType> {GoodsType.Electronics}
                },
                new Planet
                {
                    Name = "Treunus",
                    Position = new Vector2(-300, -300),
                    Image = "12601",
                    Color = ColorHelper.GetColor(116, 205, 230),
                    Description = "Description",
                    Interference = 0,
                    Radiation = 0,
                    Export = new List<ShopGoods>
                    {
                        new ShopGoods {Id = GoodsId.Smartphone, Min = 20, Max = 80, Availability = 1}
                    },
                    Import = new List<GoodsType> {GoodsType.Food}
                },
                new Station
                {
                    Name = "SS-500",
                    Position = new Vector2(200, 100),
                    Image = "Station2",
                    Description = "Description",
                    Interference = 0,
                    Radiation = 0,
                    Equipment = new List<ShopEquipment>
                    {
                        new ShopEquipment {Id = EquipmentId.MassKit, Min = 2, Max = 4, Availability = 1},
                        new ShopEquipment {Id = EquipmentId.JetEngine, Min = 1, Max = 2, Availability = 1}
                    },
                    Ships = new List<ShopShip>
                    {
                        new ShopShip { Id = ShipId.ST400, Max = 1, Availability = 1 },
                        new ShopShip { Id = ShipId.ST500, Max = 1, Availability = 1 },
                        new ShopShip { Id = ShipId.ST800, Max = 1, Availability = 1 }
                    }
                },
                new Asteroid
                {
                    Name = "A100200",
                    Position = new Vector2(-200, -200),
                    Image = "A01",
                    Description = "Description",
                    Interference = 0,
                    Radiation = 0,
                    Lodes = new List<Lode>
                    {
                        new Lode
                        {
                            Mineral = GoodsId.Ferrum,
                            Class = LodeClass.A,
                            Size = 40
                        },
                        new Lode
                        {
                            Mineral = GoodsId.Titanium,
                            Class = LodeClass.B,
                            Size = 10,
                            Radius = 250,
                            Speed = 0.4f
                        },
                        new Lode
                        {
                            Mineral = GoodsId.Ferrum,
                            Class = LodeClass.A,
                            Size = 8,
                            Radius = 400,
                            Speed = 0.6f
                        },
                        new Lode
                        {
                            Mineral = GoodsId.Titanium,
                            Class = LodeClass.B,
                            Size = 5,
                            Radius = 350,
                            Speed = 0.8f
                        },
                    }
                }
            }
        };
    }
}