using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Pointer Press Handler")]
    public class PointerPressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected UnityEvent<PointerEventData> _onPress = new UnityEvent<PointerEventData>();
        [SerializeField] protected UnityEvent<PointerEventData> _onRelease = new UnityEvent<PointerEventData>();

        private bool _isPressed;
        private PointerEventData _cache;

        public UnityEvent<PointerEventData> OnPress
            => _onPress;

        public UnityEvent<PointerEventData> OnRelease
            => _onRelease;

        public bool IsPressed
            => _isPressed;

        public void OnPointerDown(PointerEventData data)
        {
            _isPressed = true;

            _onPress.Invoke(data);

            _cache = data;
        }

        public void OnPointerUp(PointerEventData data)
        {
            _isPressed = false;

            _onRelease.Invoke(data);

            _cache = data;
        }

        public void RemoveAllListeners()
        {
            _onPress.RemoveAllListeners();
            _onRelease.RemoveAllListeners();
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