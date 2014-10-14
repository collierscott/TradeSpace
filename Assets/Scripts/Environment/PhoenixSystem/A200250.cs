using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Environment.PhoenixSystem
{
    internal static partial class Phoenix
    {
        public static readonly Asteroid A200250 = new Asteroid
        {
            Name = "A200250",
            Position = new Vector2(200, -250),
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
                    Speed = 10
                },
                new AsteroidPart
                {
                    Mineral = GoodsId.Titanium,
                    Class = AsteroidClass.B,
                    Size = 5,
                    Speed = 20
                }
            }
        };
    }
}