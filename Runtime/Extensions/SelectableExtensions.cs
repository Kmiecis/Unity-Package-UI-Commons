using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.UI.Extensions
{
    public static class SelectableExtensions
    {
        public static bool IsSelected(this Selectable self)
        {
            var system = EventSystem.current;
            if (system == null)
                return false;

            return system.currentSelectedGameObject == self.gameObject;
        }

        public static void Deselect(this Selectable self)
        {
            var system = EventSystem.current;
            if (system == null || system.currentSelectedGameObject != self.gameObject)
                return;

            system.SetSelectedGameObject(null);
        }
    }
}