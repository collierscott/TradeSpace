using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class Geometry
    {
        public static bool IntersectionLineCircle(Vector2 center, float radius, Vector2 start, Vector2 end)
        {
            Vector2 intersection1, intersection2;

            return FindLineCircleIntersections(center, radius, start, end, out intersection1, out intersection2) > 0;
        }

        public static int FindLineCircleIntersections(Vector2 center, float radius, Vector2 start, Vector2 end, out Vector2 intersection1, out Vector2 intersection2)
        {
            var dx = end.x - start.x;
            var dy = end.y - start.y;
            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (start.x - center.x) + dy * (start.y - center.y));
            var c = (start.x - center.x) * (start.x - center.x) + (start.y - center.y) * (start.y - center.y) - radius * radius;
            var det = b * b - 4 * a * c;

            if ((a <= 0.0000001) || (det < 0))
            {
                intersection1 = new Vector2(float.NaN, float.NaN);
                intersection2 = new Vector2(float.NaN, float.NaN);

                return 0;
            }

            if (det == 0)
            {
                var t = -b / (2 * a);

                intersection1 = new Vector2(start.x + t * dx, start.y + t * dy);
                intersection2 = new Vector2(float.NaN, float.NaN);

                return 1;
            }
            else
            {
                var t = (float)((-b + Math.Sqrt(det)) / (2 * a));
                
                intersection1 = new Vector2(start.x + t * dx, start.y + t * dy);
                t = (float)((-b - Math.Sqrt(det)) / (2 * a));
                intersection2 = new Vector2(start.x + t * dx, start.y + t * dy);

                return 2;
            }
        }
    }
}