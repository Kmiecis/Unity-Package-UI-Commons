using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    [RequireComponent(typeof(AspectRatioFitter))]
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Aspect Ratio Fitter Element")]
    public class AspectRatioFitterElement : LayoutElement
    {
        [SerializeField] private bool _useMinWidth;
        [SerializeField] private bool _useMinHeight;
        [SerializeField] private bool _usePreferredWidth;
        [SerializeField] private bool _usePreferredHeight;

        private AspectRatioFitter _fitter;

        public override float minWidth
        {
            get => GetMinWidth();
        }

        public override float minHeight
        {
            get => GetMinHeight();
        }

        public override float preferredWidth
        {
            get => GetPreferredWidth();
        }

        public override float preferredHeight
        {
            get => GetPreferredHeight();
        }

        private RectTransform RectTransform
        {
            get => (RectTransform)transform;
        }

        private AspectRatioFitter Fitter
        {
            get => _fitter != null ? _fitter : (_fitter = GetComponent<AspectRatioFitter>());
        }

        private float GetMinWidth()
        {
            if (!_useMinWidth)
                return -1.0f;

            var size = RectTransform.rect.size;

            if (Fitter.aspectMode == AspectRatioFitter.AspectMode.HeightControlsWidth)
                return size.y * Fitter.aspectRatio;

            return size.x;
        }

        private float GetMinHeight()
        {
            if (!_useMinHeight)
                return -1.0f;

            var size = RectTransform.rect.size;

            if (Fitter.aspectMode == AspectRatioFitter.AspectMode.WidthControlsHeight)
                return size.x / Fitter.aspectRatio;

            return size.y;
        }

        private float GetPreferredWidth()
        {
            if (!_usePreferredWidth)
                return -1.0f;

            var size = RectTransform.rect.size;

            if (Fitter.aspectMode == AspectRatioFitter.AspectMode.HeightControlsWidth)
                return size.y * Fitter.aspectRatio;

            return size.x;
        }

        private float GetPreferredHeight()
        {
            if (!_usePreferredHeight)
                return -1.0f;

            var size = RectTransform.rect.size;

            if (Fitter.aspectMode == AspectRatioFitter.AspectMode.WidthControlsHeight)
                return size.x / Fitter.aspectRatio;

            return size.y;
        }
    }
}