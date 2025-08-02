using Common.UI.Extensions;
using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Common.UI
{
    [Preserve]
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Scaled Text")]
    public class ScaledText : Text
    {
        [SerializeField] protected float _fontScale;

        public float FontScale
        {
            get => _fontScale;
            set => SetProperty(ref _fontScale, value, UpdateFontSize);
        }

        protected override void OnDidApplyAnimationProperties()
        {
            OnBecameDirty();
            base.OnDidApplyAnimationProperties();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            OnBecameDirty();
            base.OnRectTransformDimensionsChange();
        }

        protected override void OnTransformParentChanged()
        {
            OnBecameDirty();
            base.OnTransformParentChanged();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            OnBecameDirty();
            base.OnValidate();
        }
#endif

        private void UpdateFontSize(float scale)
        {
            var size = Mathf.RoundToInt(rectTransform.GetRealSize().y * scale);
            fontSize = size;
            resizeTextMaxSize = size;
        }

        private void SetProperty(ref float property, float value, Action<float> callback)
        {
            if (!Mathf.Approximately(property, value))
            {
                property = value;
                callback(value);
            }
        }

        private void OnBecameDirty()
        {
            UpdateFontSize(_fontScale);
        }
    }
}