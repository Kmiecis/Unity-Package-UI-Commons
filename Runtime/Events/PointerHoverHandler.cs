using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Pointer Hover Handler")]
    public class PointerHoverHandler : PointerHandlerBase, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
    {
        [SerializeField] protected UnityEvent<PointerEventData> _onHoverBegan = new UnityEvent<PointerEventData>();
        [SerializeField] protected UnityEvent<PointerEventData> _onHovering = new UnityEvent<PointerEventData>();
        [SerializeField] protected UnityEvent<PointerEventData> _onHoverEnded = new UnityEvent<PointerEventData>();

        protected bool _isHovering;
        protected PointerEventData _cache;

        public UnityEvent<PointerEventData> OnHoverBegan
            => _onHoverBegan;

        public UnityEvent<PointerEventData> OnHovering
            => _onHovering;

        public UnityEvent<PointerEventData> OnHoverEnded
            => _onHoverEnded;

        public bool IsHovering
            => _isHovering;

        public virtual void OnPointerEnter(PointerEventData data)
        {
            _isHovering = true;

            _onHoverBegan.Invoke(data);

            UseIfNecessary(data);
            
            _cache = data;
        }

        public virtual void OnPointerMove(PointerEventData data)
        {
            _onHovering.Invoke(data);

            UseIfNecessary(data);

            _cache = data;
        }

        public virtual void OnPointerExit(PointerEventData data)
        {
            _isHovering = false;

            _onHoverEnded.Invoke(data);

            UseIfNecessary(data);

            _cache = data;
        }

        public virtual void RemoveAllListeners()
        {
            _onHoverBegan.RemoveAllListeners();
            _onHovering.RemoveAllListeners();
            _onHoverEnded.RemoveAllListeners();
        }

        #region Unity
        protected void OnDisable()
        {
            if (_isHovering)
            {
                OnPointerExit(_cache);
            }
        }

        protected void OnDestroy()
        {
            RemoveAllListeners();
        }
        #endregion
    }
}