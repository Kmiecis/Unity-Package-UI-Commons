using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Pointer Click Handler")]
    public class PointerClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected UnityEvent<PointerEventData> _onClicked = new UnityEvent<PointerEventData>();

        public UnityEvent<PointerEventData> OnClicked
            => _onClicked;

        public virtual void OnPointerClick(PointerEventData data)
        {
            _onClicked.Invoke(data);
        }

        public virtual void RemoveAllListeners()
        {
            _onClicked.RemoveAllListeners();
        }

        #region Unity
        protected void OnDestroy()
        {
            RemoveAllListeners();
        }
        #endregion
    }
}