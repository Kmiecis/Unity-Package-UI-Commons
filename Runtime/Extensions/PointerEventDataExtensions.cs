using UnityEngine.EventSystems;

namespace Common.UI
{
    public static class PointerEventDataExtensions
    {
        public static bool IsLeftButton(this PointerEventData self)
        {
            return self.button == PointerEventData.InputButton.Left;
        }

        public static bool IsRightButton(this PointerEventData self)
        {
            return self.button == PointerEventData.InputButton.Right;
        }

        public static bool IsMiddleButton(this PointerEventData self)
        {
            return self.button == PointerEventData.InputButton.Middle;
        }
    }
}