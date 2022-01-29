using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Interface
{
    [AddComponentMenu("UI/Effects/StepShadow")]
    public class StepShadow : BaseMeshEffect
    {
        [SerializeField] private int steps = 3;
        [SerializeField] private Color shadowColor = new Color(0f, 0f, 0f, 0.25f);
        [SerializeField] private Vector2 shadowSpread = new Vector2(10,10);
        [SerializeField] private Vector2 shadowDistance = Vector2.zero;

        private void DropShadowEffect(ICollection<UIVertex> verts)
        {
            int count = verts.Count;

            List<UIVertex> vertsCopy = new List<UIVertex>(verts);
            verts.Clear();

            for (int i = 0; i <= steps; i++)
            {
                for (int v = 0; v < count; v++)
                {
                    UIVertex vertex = vertsCopy[v];
                    Vector3 position = vertex.position;
                    float fac = i / (float)steps;
                    position.x *= (1 + shadowSpread.x * fac * 0.01f);
                    position.y *= (1 + shadowSpread.y * fac * 0.01f);
                    position.x += shadowDistance.x * fac;
                    position.y += shadowDistance.y * fac;
                    vertex.position = position;
                    Color32 color = shadowColor;
                    color.a = (byte)(color.a / (float)steps);
                    vertex.color = color;
                    verts.Add(vertex);
                }
            }

            foreach (UIVertex uiVertex in vertsCopy)
            {
                verts.Add(uiVertex);
            }
        }

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
                return;

            List<UIVertex> output = new List<UIVertex>();
            vh.GetUIVertexStream(output);

            DropShadowEffect(output);

            vh.Clear();
            vh.AddUIVertexTriangleStream(output);
        }
    }
}