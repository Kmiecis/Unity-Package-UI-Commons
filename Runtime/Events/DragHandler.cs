using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/" + "Drag Handler")]
    public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private UnityEvent<PointerEventData> _onDragBegan = new UnityEvent<PointerEventData>();
        [SerializeField] private UnityEvent<PointerEventData> _onDragging = new UnityEvent<PointerEventData>();
        [SerializeField] private UnityEvent<PointerEventData> _onDragEnded = new UnityEvent<PointerEventData>();

        private bool _isDragging;
        private PointerEventData _cache;

        public UnityEvent<PointerEventData> OnDragBegan
            => _onDragBegan;

        public UnityEvent<PointerEventData> OnDragging
            => _onDragging;

        public UnityEvent<PointerEventData> OnDragEnded
            => _onDragEnded;

        public bool IsDragging
            => _isDragging;

        public void OnBeginDrag(PointerEventData data)
        {
            _isDragging = true;

            _onDragBegan.Invoke(data);
            _cache = data;
        }

        public void OnDrag(PointerEventData data)
        {
            _onDragging.Invoke(data);
            _cache = data;
        }

        public void OnEndDrag(PointerEventData data)
        {
            _onDragEnded.Invoke(data);
            _cache = data;

            _isDragging = false;
        }

        public void RemoveAllListeners()
        {
            _onDragBegan.RemoveAllListeners();
            _onDragging.RemoveAllListeners();
            _onDragEnded.RemoveAllListeners();
        }

        #region Unity
        private void OnDisable()
        {
            if (_isDragging)
            {
                OnEndDrag(_cache);
            }
        }

        private void OnDestroy()
        {
            RemoveAllListeners();
        }
        #endregion
    }
}