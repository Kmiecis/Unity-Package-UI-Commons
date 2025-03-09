using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    public class AnyLayoutGroup : HorizontalOrVerticalLayoutGroup
    {
        public enum EType
        {
            Horizontal = 0,
            Vertical = 1
        }

        [SerializeField] protected EType _type;

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