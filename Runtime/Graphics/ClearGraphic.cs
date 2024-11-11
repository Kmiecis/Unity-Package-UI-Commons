using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class ClearGraphic : Graphic
    {
        public override void SetMaterialDirty()
        {
        }

        public override void SetVerticesDirty()
        {
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}