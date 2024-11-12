using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/" + "Click Handler")]
    public class ClickHandler : PointerHandlerBase, IPointerClickHandler
    {
        [SerializeField] protected UnityEvent<PointerEventData> _onClick = new UnityEvent<PointerEventData>();

        public UnityEvent<PointerEventData> OnClick
            => _onClick;

        public void OnPointerClick(PointerEventData data)
        {
            _onClick.Invoke(data);

            UseIfNecessary(data);
        }

        public void RemoveAllListeners()
        {
            _onClick.RemoveAllListeners();
        }

        #region Unity
        private void OnDestroy()
        {
            RemoveAllListeners();
        }
        #endregion
    }
}