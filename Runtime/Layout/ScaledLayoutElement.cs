using Common.UI.Extensions;
using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Common.UI
{
    [Preserve]
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Scaled Layout Element")]
    public class ScaledLayoutElement : LayoutElement
    {
        [SerializeField] protected float _minWidthScale = -1.0f;
        [SerializeField] protected float _minHeightScale = -1.0f;
        [SerializeField] protected float _preferredWidthScale = -1.0f;
        [SerializeField] protected float _preferredHeightScale = -1.0f;

        protected RectTransform rectTransform
        {
            get => transform as RectTransform;
        }

        public Vector2 parentSize
        {
            get => rectTransform.GetParentRealSize();
        }

        public override float minWidth
        {
            get => _minWidthScale * parentSize.x;
            set => throw new InvalidOperationException(
                $"Setting {nameof(ScaledLayoutElement)}.{nameof(minWidth)} directly is forbidden. Use {nameof(MinWidthScale)}");
        }

        public override float minHeight
        {
            get => _minHeightScale * parentSize.y;
            set => throw new InvalidOperationException(
                $"Setting {nameof(ScaledLayoutElement)}.{nameof(minHeight)} directly is forbidden. Use {nameof(MinHeightScale)}");
        }

        public override float preferredWidth
        {
            get => _preferredWidthScale * parentSize.x;
            set => throw new InvalidOperationException(
                $"Setting {nameof(ScaledLayoutElement)}.{nameof(preferredWidth)} directly is forbidden. Use {nameof(PreferredWidthScale)}");
        }

        public override float preferredHeight
        {
            get => _preferredHeightScale * parentSize.y;
            set => throw new InvalidOperationException(
                $"Setting {nameof(ScaledLayoutElement)}.{nameof(preferredHeight)} directly is forbidden. Use {nameof(PreferredHeightScale)}");
        }

        public virtual float MinWidthScale
        {
            get => _minWidthScale;
            set => SetFloat(ref _minWidthScale, value);
        }

        public virtual float MinHeightScale
        {
            get => _minHeightScale;
            set => SetFloat(ref _minHeightScale, value);
        }

        public virtual float PreferredWidthScale
        {
            get => _preferredWidthScale;
            set => SetFloat(ref _preferredWidthScale, value);
        }

        public virtual float PreferredHeightScale
        {
            get => _preferredHeightScale;
            set => SetFloat(ref _preferredHeightScale, value);
        }

        protected override void OnRectTransformDimensionsChange()
        {
            SetDirty();
        }

        private void SetFloat(ref float property, float value)
        {
            if (!Mathf.Approximately(property, value))
            {
                property = value;

                SetDirty();
            }
        }
    }
}