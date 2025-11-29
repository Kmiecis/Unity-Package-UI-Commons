using Common.UI.Extensions;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Common.UI
{
    [Preserve]
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Aspect Layout Element")]
    public class AspectLayoutElement : LayoutElement
    {
        public enum AspectMode
        {
            None,
            WidthControlsHeight,
            HeightControlsWidth
        }

        [SerializeField] private AspectMode _minAspectMode;
        [SerializeField] private float _minAspectRatio = -1.0f;
        [SerializeField] private AspectMode _preferredAspectMode;
        [SerializeField] private float _preferredAspectRatio = -1.0f;

        public AspectMode MinAspectMode
        {
            get => _minAspectMode;
            set
            {
                _minAspectMode = value;
                UpdateLayoutElementParams();
            }
        }

        public AspectMode PreferredAspectMode
        {
            get => _preferredAspectMode;
            set
            {
                _preferredAspectMode = value;
                UpdateLayoutElementParams();
            }
        }

        protected RectTransform rectTransform
        {
            get => transform as RectTransform;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
        }

        private void UpdateLayoutElementParams()
        {
            UpdateMinLayoutElementParams();
            UpdatePreferredLayoutElementParams();
        }

        private void UpdateMinLayoutElementParams()
        {
            switch (_minAspectMode)
            {
                case AspectMode.HeightControlsWidth:
                    minHeight = -1.0f;
                    minWidth = GetParentSize().y * _minAspectRatio;
                    break;

                case AspectMode.WidthControlsHeight:
                    minHeight = GetParentSize().x / _minAspectRatio;
                    minWidth = -1.0f;
                    break;

                default:
                    minHeight = -1.0f;
                    minWidth = -1.0f;
                    break;
            }
        }

        private void UpdatePreferredLayoutElementParams()
        {
            switch (_preferredAspectMode)
            {
                case AspectMode.HeightControlsWidth:
                    preferredHeight = -1.0f;
                    preferredWidth = GetParentSize().y * _preferredAspectRatio;
                    break;

                case AspectMode.WidthControlsHeight:
                    preferredHeight = GetParentSize().x / _preferredAspectRatio;
                    preferredWidth = -1.0f;
                    break;

                default:
                    preferredHeight = -1.0f;
                    preferredWidth = -1.0f;
                    break;
            }
        }

        private Vector2 GetParentSize()
        {
            var result = rectTransform.GetParentRealSize();
            if (rectTransform.TryGetComponent<LayoutGroup>(out var layoutGroup))
            {
                result.y -= layoutGroup.padding.vertical;
                result.x -= layoutGroup.padding.horizontal;
            }
            return result;
        }

        protected override void OnValidate()
        {
            UpdateLayoutElementParams();

            base.OnValidate();
        }
    }
}