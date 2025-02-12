using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.UI.Extensions
{
    public static class ButtonExtensions
    {
        public static void Click(this Button self)
        {
            ExecuteEvents.Execute(self.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }
}