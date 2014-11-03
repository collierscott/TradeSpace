using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.AndromedaSystem
{
    internal static partial class Andromeda
    {
        public static readonly Asteroid A100200 = new Asteroid
        {
            Name = "A100200",
            Position = new Vector2(-100, -200),
            Image = "A01",
            Description = "Description",
            Interference = 0,
            Radiation = 0,
            Parts = new List<AsteroidPart>
            {
                new AsteroidPart
                {
                    Mineral = GoodsId.Ferrum,
                    Class = AsteroidClass.A,
                    Size = 10,
                    Speed = 10,
                    Quantity = 10,
                },
                new AsteroidPart
                {
                    Mineral = GoodsId.Titanium,
                    Class = AsteroidClass.B,
                    Size = 5,
                    Speed = 20,
                    Quantity = 100,
                },
                 new AsteroidPart
                {
                    Mineral = GoodsId.Ferrum,
                    Class = AsteroidClass.A,
                    Size = 20,
                    Speed = -10,
                    Quantity = 10,
                },
                 new AsteroidPart
                {
                    Mineral = GoodsId.Titanium,
                    Class = AsteroidClass.B,
                    Size = 5,
                    Speed = 20,
                    Quantity = 1,
                },
            }
        };
    }
}