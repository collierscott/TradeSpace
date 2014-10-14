using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class Geometry
    {
        public static bool IntersectionLineCircle(Vector2 center, float radius, Vector2 start, Vector2 end)
        {
            List<Vector2> points;

            return IntersectionLineCircle(center, radius, start, end, out points);
        }

        public static bool IntersectionLineCircle(Vector2 center, float radius, Vector2 start, Vector2 end, out List<Vector2> result)
        {
            result = new List<Vector2>();

            var ax = (end.y - start.y) / (end.x - start.x);
            var bx = start.y - ax * start.x;
            var a = 1 + ax * ax;
            var b = 2 * (ax * bx - ax * center.y - center.x);
            var c = center.x * center.x + bx * bx + center.y * center.y - radius * radius - 2 * bx * center.y;
            var f = b * b - 4 * a * c;

            if (f < 0)
            {
                return false;
            }

            var sqrt = Mathf.Sqrt(f);
            var signs = Math.Abs(sqrt) < 0.001 ? new[] { 0 } : new[] { -1, 1 };

            foreach (var sign in signs)
            {
                var x = (-b + sign * sqrt) / (2 * a);
                var y = ax * x + bx;

                result.Add(new Vector2(x, y));
            }

            return true;
        }
    }
}