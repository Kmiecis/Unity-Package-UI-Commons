using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common.UI
{
    [AddComponentMenu(nameof(Common) + "/" + nameof(UI) + "/Pointer Delayed Hover Handler")]
    public class PointerDelayedHoverHandler : PointerHoverHandler
    {
        [SerializeField] protected float _delay = 0.5f;

        protected Coroutine _coroutine;

        public override void OnPointerEnter(PointerEventData data)
        {
            if (_delay > 0.0f)
            {
                _coroutine = StartCoroutine(DelayedOnPointerEnter());
            }
            else
            {
                base.OnPointerEnter(data);
            }

            _cache = data;
        }

        public override void OnPointerMove(PointerEventData data)
        {
            if (_isHovering)
            {
                base.OnPointerMove(data);
            }

            _cache = data;
        }

        public override void OnPointerExit(PointerEventData data)
        {
            if (_isHovering)
            {
                base.OnPointerExit(data);
            }

            _cache = data;

            CloseCoroutine();
        }

        private IEnumerator DelayedOnPointerEnter()
        {
            yield return new WaitForSecondsRealtime(_delay);

            base.OnPointerEnter(_cache);

            CloseCoroutine();
        }

        private void CloseCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);

                _coroutine = null;
            }
        }
    }
}