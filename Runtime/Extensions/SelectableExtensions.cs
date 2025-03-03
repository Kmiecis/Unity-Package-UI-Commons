using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.UI
{
    public static class SelectableExtensions
    {
        public static void Unselect(this Selectable self)
        {
            if (EventSystem.current == null || EventSystem.current.currentSelectedGameObject != self.gameObject)
                return;

            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}