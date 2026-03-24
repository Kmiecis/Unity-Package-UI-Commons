using UnityEngine;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Pointer Used Click Handler")]
    public class PointerUsedClickHandler : PointerClickHandler
    {
        [SerializeField] protected bool _skipUsed;
        [SerializeField] protected bool _makeUsed;

        public override void OnPointerClick(PointerEventData data)
        {
            if (!_skipUsed || !data.used)
            {
                base.OnPointerClick(data);
            }

            if (_makeUsed)
            {
                data.Use();
            }
        }
    }
}