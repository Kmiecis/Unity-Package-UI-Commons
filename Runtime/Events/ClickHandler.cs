using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    public class ClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent<PointerEventData> _onClick;

        public UnityEvent<PointerEventData> OnClick
            => _onClick;

        public void OnPointerClick(PointerEventData data)
        {
            _onClick.Invoke(data);
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