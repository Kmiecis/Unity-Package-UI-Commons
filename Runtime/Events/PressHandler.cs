using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    public class PressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private UnityEvent<PointerEventData> _onPressBegan;
        [SerializeField] private UnityEvent<PointerEventData> _onPressEnded;

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
        }

        public void OnPointerUp(PointerEventData data)
        {
            _onPressEnded.Invoke(data);
            _cache = data;

            _isPressed = false;
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