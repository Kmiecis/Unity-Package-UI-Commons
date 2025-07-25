using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/" + nameof(PointerHoverHandler))]
    public class PointerHoverHandler : PointerHandlerBase, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected UnityEvent<PointerEventData> _onHoverBegan = new UnityEvent<PointerEventData>();
        [SerializeField] protected UnityEvent<PointerEventData> _onHoverEnded = new UnityEvent<PointerEventData>();

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

            UseIfNecessary(data);
        }

        public void OnPointerExit(PointerEventData data)
        {
            _isHovering = false;

            _onHoverEnded.Invoke(data);
            _cache = data;

            UseIfNecessary(data);
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