using UnityEngine.EventSystems;

namespace Common.UI.Extensions
{
    public static class PointerEventDataExtensions
    {
        public static bool IsLeftClick(this PointerEventData self)
        {
            return self.button == PointerEventData.InputButton.Left;
        }

        public static bool IsRightClick(this PointerEventData self)
        {
            return self.button == PointerEventData.InputButton.Right;
        }

        public static bool IsMiddleClick(this PointerEventData self)
        {
            return self.button == PointerEventData.InputButton.Middle;
        }
    }
}