#pragma warning disable 0649
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
    public class ScriptableEventListener : MonoBehaviour
    {
        [SerializeField] private ScriptableEvent scriptableEvent;
        
        public UnityEvent unityEvent;

        private void OnEnable()
        {
            if (scriptableEvent)
                scriptableEvent.Subscribe(unityEvent.Invoke);
        }

        private void OnDisable()
        {
            if (scriptableEvent)
                scriptableEvent.Unsubscribe(unityEvent.Invoke);
        }
    }
}