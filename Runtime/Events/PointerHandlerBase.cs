using UnityEngine.EventSystems;
using UnityEngine;

namespace Common.UI
{
    public abstract class PointerHandlerBase : MonoBehaviour
    {
        [SerializeField] private bool _use;

        protected void UseIfNecessary(PointerEventData data)
        {
            if (_use) data.Use();
        }
    }
}