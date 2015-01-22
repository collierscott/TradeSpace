using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.Systems
{
    public partial class SpaceSystems
    {
        public static SpaceSystem Beta = new SpaceSystem
        {
            Name = Env.SystemNames.Beta,
            Position = new Vector2(700, 260),
            Color = ColorHelper.GetColor("#FF6600", 180),
            Locations = new List<Location>
            {
                new Gates { ConnectedSystem = Env.SystemNames.Alpha },
                new Gates { ConnectedSystem = Env.SystemNames.Gamma },
                new Gates { ConnectedSystem = Env.SystemNames.Sirius },
                new Planet
                {
                    Name = "Duster",
                    Position = new Vector2(400, -100),
                    Image = "18912",
                    Color = ColorHelper.GetColor(180, 180, 0),
                    Description = "Description",
                    Interference = 0,
                    Radiation = 90,
                    Export = new List<ShopGoods>
                    {
                        new ShopGoods {Id = GoodsId.Smartphone, Min = 20, Max = 80, Availability = 1}
                    },
                    Import = new List<GoodsType> {GoodsType.Food}
                },
            }
        };
    }
}