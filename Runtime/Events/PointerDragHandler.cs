using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Pointer Drag Handler")]
    public class PointerDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] protected UnityEvent<PointerEventData> _onDragBegan = new UnityEvent<PointerEventData>();
        [SerializeField] protected UnityEvent<PointerEventData> _onDragging = new UnityEvent<PointerEventData>();
        [SerializeField] protected UnityEvent<PointerEventData> _onDragEnded = new UnityEvent<PointerEventData>();

        protected bool _isDragging;
        protected PointerEventData _cache;

        public UnityEvent<PointerEventData> OnDragBegan
            => _onDragBegan;

        public UnityEvent<PointerEventData> OnDragging
            => _onDragging;

        public UnityEvent<PointerEventData> OnDragEnded
            => _onDragEnded;

        public bool IsDragging
            => _isDragging;

        public virtual void OnBeginDrag(PointerEventData data)
        {
            _isDragging = true;

            _onDragBegan.Invoke(data);

            _cache = data;
        }

        public virtual void OnDrag(PointerEventData data)
        {
            _onDragging.Invoke(data);

            _cache = data;
        }

        public virtual void OnEndDrag(PointerEventData data)
        {
            _isDragging = false;

            _onDragEnded.Invoke(data);

            _cache = data;
        }

        public virtual void RemoveAllListeners()
        {
            _onDragBegan.RemoveAllListeners();
            _onDragging.RemoveAllListeners();
            _onDragEnded.RemoveAllListeners();
        }

        #region Unity
        protected void OnDisable()
        {
            if (_isDragging)
            {
                OnEndDrag(_cache);
            }
        }

        protected void OnDestroy()
        {
            RemoveAllListeners();
        }
        #endregion
    }
}