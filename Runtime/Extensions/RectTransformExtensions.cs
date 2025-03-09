using UnityEngine;

namespace Common.UI.Extensions
{
    public static class RectTransformExtensions
    {
        public static Vector2 GetRealSize(this RectTransform self)
        {
            var anchorMin = self.anchorMin;
            var anchorMax = self.anchorMax;

            var size = self.rect.size;
            var sizeDelta = self.sizeDelta;

            if (!Mathf.Approximately(anchorMin.x, anchorMax.x))
                size.x -= sizeDelta.x;
            if (!Mathf.Approximately(anchorMin.y, anchorMax.y))
                size.y -= sizeDelta.y;
            return size;
        }

        public static Vector2 GetParentRealSize(this RectTransform self)
        {
            var parent = self.parent as RectTransform;
            if (parent != null) return parent.GetRealSize();
            return new Vector2(Screen.width, Screen.height);
        }

        public static Vector2 GetParentSize(this RectTransform self)
        {
            var parent = self.parent as RectTransform;
            if (parent != null) return parent.rect.size;
            return new Vector2(Screen.width, Screen.height);
        }

        public static Rect GetRealRect(this RectTransform self)
        {
            var size = Vector2.Scale(self.rect.size, self.lossyScale);
            var rect = new Rect(self.position.x, Screen.height - self.position.y, size.x, size.y);
            rect.x -= self.pivot.x * size.x;
            rect.y -= (1.0f - self.pivot.y) * size.y;
            return rect;
        }
    }
}