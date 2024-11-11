using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private UnityEvent<PointerEventData> _onHoverBegan;
        [SerializeField] private UnityEvent<PointerEventData> _onHoverEnded;

        private bool _isHovering;
        private PointerEventData _cache;

        public UnityEvent<PointerEventData> OnHoverBegan
            => _onHoverBegan;

        public UnityEvent<PointerEventData> OnHoverEnded
            => _onHoverEnded;

        public bool IsHovering
            => _isHovering;

        public void OnPointerEnter(PointerEventData data)
        {
            _isHovering = true;

            _onHoverBegan.Invoke(data);
            _cache = data;
        }

        public void OnPointerExit(PointerEventData data)
        {
            _onHoverEnded.Invoke(data);
            _cache = data;

            _isHovering = false;
        }

        public void RemoveAllListeners()
        {
            _onHoverBegan.RemoveAllListeners();
            _onHoverEnded.RemoveAllListeners();
        }

        #region Unity
        private void OnDisable()
        {
            if (_isHovering)
            {
                OnPointerExit(_cache);
            }
        }

        private void OnDestroy()
        {
            RemoveAllListeners();
        }
        #endregion
    }
}