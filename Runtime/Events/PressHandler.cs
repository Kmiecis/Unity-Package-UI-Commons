using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/" + "Press Handler")]
    public class PressHandler : PointerHandlerBase, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected UnityEvent<PointerEventData> _onPressBegan = new UnityEvent<PointerEventData>();
        [SerializeField] protected UnityEvent<PointerEventData> _onPressEnded = new UnityEvent<PointerEventData>();

        private bool _isPressed;
        private PointerEventData _cache;

        public UnityEvent<PointerEventData> OnPressBegan
            => _onPressBegan;

        public UnityEvent<PointerEventData> OnPressEnded
            => _onPressEnded;

        public bool IsPressed
            => _isPressed;

        public void OnPointerDown(PointerEventData data)
        {
            _isPressed = true;

            _onPressBegan.Invoke(data);
            _cache = data;

            UseIfNecessary(data);
        }

        public void OnPointerUp(PointerEventData data)
        {
            _onPressEnded.Invoke(data);
            _cache = data;

            _isPressed = false;

            UseIfNecessary(data);
        }

        public void RemoveAllListeners()
        {
            _onPressBegan.RemoveAllListeners();
            _onPressEnded.RemoveAllListeners();
        }

        #region Unity
        private void OnDisable()
        {
            if (_isPressed)
            {
                OnPointerUp(_cache);
            }
        }

        private void OnDestroy()
        {
            RemoveAllListeners();
        }
        #endregion
    }
}