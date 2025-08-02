using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Pointer Click Handler")]
    public class PointerClickHandler : PointerHandlerBase, IPointerClickHandler
    {
        [SerializeField] protected UnityEvent<PointerEventData> _onClicked = new UnityEvent<PointerEventData>();

        public UnityEvent<PointerEventData> OnClicked
            => _onClicked;

        public void OnPointerClick(PointerEventData data)
        {
            _onClicked.Invoke(data);

            UseIfNecessary(data);
        }

        public void RemoveAllListeners()
        {
            _onClicked.RemoveAllListeners();
        }

        #region Unity
        private void OnDestroy()
        {
            RemoveAllListeners();
        }
        #endregion
    }
}