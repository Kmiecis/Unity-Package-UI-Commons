using Common.UI.Extensions;
using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Common.UI
{
    [Preserve]
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Scaled Layout Group")]
    public class ScaledLayoutGroup : HorizontalOrVerticalLayoutGroup
    {
        public enum EType
        {
            Horizontal = 0,
            Vertical = 1
        }

        [SerializeField] protected EType _type;
        [SerializeField] protected float _scaledPaddingLeft;
        [SerializeField] protected float _scaledPaddingRight;
        [SerializeField] protected float _scaledPaddingTop;
        [SerializeField] protected float _scaledPaddingBottom;
        [SerializeField] protected float _scaledSpacing;

        public Vector2 parentSize
        {
            get => rectTransform.GetParentRealSize();
        }

        public EType Type
        {
            get => _type;
            set => _type = value;
        }

        public bool IsHorizontal
        {
            get => _type == EType.Horizontal;
            set => _type = value ? EType.Horizontal : EType.Vertical;
        }

        public bool IsVertical
        {
            get => _type == EType.Vertical;
            set => _type = value ? EType.Vertical : EType.Horizontal;
        }

        public float ScaledPaddingLeft
        {
            get => _scaledPaddingLeft;
            set => SetProperty(ref _scaledPaddingLeft, value, UpdatePaddingLeft);
        }

        public float ScaledPaddingRight
        {
            get => _scaledPaddingRight;
            set => SetProperty(ref _scaledPaddingRight, value, UpdatePaddingRight);
        }

        public float ScaledPaddingTop
        {
            get => _scaledPaddingTop;
            set => SetProperty(ref _scaledPaddingTop, value, UpdatePaddingTop);
        }

        public float ScaledPaddingBottom
        {
            get => _scaledPaddingBottom;
            set => SetProperty(ref _scaledPaddingBottom, value, UpdatePaddingBottom);
        }

        public float ScaledSpacing
        {
            get => _scaledSpacing;
            set => SetProperty(ref _scaledSpacing, value, UpdateSpacing);
        }

        public new float spacing
        {
            get => parentSize [(int)_type] * _scaledSpacing;
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

        private void SetProperty(ref float property, float value, Action<float> callback)
        {
            if (!Mathf.Approximately(property, value))
            {
                property = value;
                callback(value);
            }
        }

        private void UpdatePaddingLeft(float scale)
        {
            m_Padding.left = Mathf.RoundToInt(parentSize.x * scale);
        }

        private void UpdatePaddingRight(float scale)
        {
            m_Padding.right = Mathf.RoundToInt(parentSize.x * scale);
        }

        private void UpdatePaddingTop(float scale)
        {
            m_Padding.top = Mathf.RoundToInt(parentSize.y * scale);
        }

        private void UpdatePaddingBottom(float scale)
        {
            m_Padding.bottom = Mathf.RoundToInt(parentSize.y * scale);
        }

        private void UpdateSpacing(float scale)
        {
            m_Spacing = parentSize[(int) _type] * scale;
        }

        private void OnBecameDirty()
        {
            UpdatePaddingLeft(_scaledPaddingLeft);
            UpdatePaddingRight(_scaledPaddingRight);
            UpdatePaddingTop(_scaledPaddingTop);
            UpdatePaddingBottom(_scaledPaddingBottom);
            UpdateSpacing(_scaledSpacing);
        }

        #region Base
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalcAlongAxis(0, IsVertical);
        }

        public override void CalculateLayoutInputVertical()
        {
            CalcAlongAxis(1, IsVertical);
        }

        public override void SetLayoutHorizontal()
        {
            SetChildrenAlongAxis(0, IsVertical);
        }

        public override void SetLayoutVertical()
        {
            SetChildrenAlongAxis(1, IsVertical);
        }
        #endregion
    }
}