using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class Geometry
    {
        public static bool IntersectionLineCircle(Vector2 center, float radius, Vector2 start, Vector2 end)
        {
            if (Vector2.Distance(center, start) <= radius || Vector2.Distance(center, end) <= radius)
            {
                return true;
            }

            var x = center.x;
            var y = center.y;

            var x0 = start.x;
            var y0 = start.y;

            var x1 = end.x;
            var y1 = end.y;

            var d = ((y0 - y1) * x + (x1 - x0) * y + (x0 * y1 - x1 * y0)) / Mathf.Sqrt(Mathf.Pow((x1 - x0), 2) + Mathf.Pow((y1 - y0), 2));

            if (d > radius)
            {
                return false;
            }

            var a = Vector2.Angle(end, end - start);
            var b = Vector2.Angle(start, start - end);

            return a < 90 && b < 90;
        }
    }
}