using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Behaviour
{
    public class NativeRenderer : Script // TODO: <ake static
    {
        public const string OrbitLine = "OrbitLine";
        public const string HyperLine = "HyperLine";
        public const string RouteLine = "RouteLine";
        public const float Transparency = 0.15f;

        public void DrawOrbit(Transform parent, Vector2 position, Color color)
        {
            const int size = 40;
            var radius = Vector2.Distance(Vector2.zero, position);
            var vertices = new List<Vector2>();

            for (var i = 0; i <= size; i++)
            {
                var angle = (float) i / size * 2 * Mathf.PI;

                vertices.Add(radius * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
            }

            DrawLine(parent, OrbitLine, vertices, 0.01f, -2, color, color.SetAlpha(0))
                .transform.Rotate(0, 0, 90 + RotationHelper.Angle(position));
        }

        public void DrawHyperLine(Transform parent, Vector2 from, Vector2 to, Color colorFrom, Color colorTo)
        {
            DrawLine(parent, HyperLine, from, to, 0.005f, -10, colorFrom, colorTo);
        }

        public void DrawRouteLine(Transform parent, Vector2 from, Vector2 to)
        {
            var color = ColorHelper.GetColor(0, 160, 255, 150);
            var middle = (from + to) / 2;

            DrawLine(parent, RouteLine, from, middle, 0.0075f, -1, color.SetAlpha(Transparency), color);
            DrawLine(parent, RouteLine, middle, to, 0.0075f, -1, color, color.SetAlpha(Transparency));
        }

        public void DrawRouteLine(Transform parent, List<Vector2> route)
        {
            var color = ColorHelper.GetColor(0, 160, 255, 150);
            var middle = route.Count / 2;

            DrawLine(parent, RouteLine, route.GetRange(0, middle + 1), 0.0075f, -1, color.SetAlpha(Transparency), color);
            DrawLine(parent, RouteLine, route.GetRange(middle, route.Count - middle), 0.0075f, -1, color, color.SetAlpha(Transparency));
        }

        private static void DrawLine(Transform parent, string marker, Vector2 from, Vector2 to,
            float width, float z, Color colorFrom, Color colorTo)
        {
            DrawLine(parent, marker, new List<Vector2> { from, to }, width, z, colorFrom, colorTo);
        }

        private static LineRenderer DrawLine(Transform parent, string marker, IList<Vector2> vertices,
            float width, float z, Color colorFrom, Color colorTo)
        {
            var instance = new GameObject(marker);

            instance.transform.parent = parent;

            var lineRenderer = instance.AddComponent<LineRenderer>();

            lineRenderer.useWorldSpace = false;
            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            lineRenderer.SetColors(colorFrom, colorTo);
            lineRenderer.SetWidth(width, width);
            lineRenderer.SetVertexCount(vertices.Count);

            for (var i = 0; i < vertices.Count; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(vertices[i].x, vertices[i].y, z));
            }

            lineRenderer.transform.localPosition = Vector2.zero;
            lineRenderer.transform.localScale = Vector3.one;

            return lineRenderer;
        }
    }
}