using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/" + nameof(ClearGraphic))]
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