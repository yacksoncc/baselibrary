#pragma warning disable 0649
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
   public class ScriptableEventListener : MonoBehaviour
   {
      [SerializeField]
      private ScriptableEventEmpty scriptableEventEmpty;

      public UnityEvent unityEvent;

      private void OnEnable()
      {
         if(scriptableEventEmpty)
            scriptableEventEmpty.Subscribe(unityEvent.Invoke);
      }

      private void OnDisable()
      {
         if(scriptableEventEmpty)
            scriptableEventEmpty.Unsubscribe(unityEvent.Invoke);
      }

      public void ExecuteEvent()
      {
         unityEvent.Invoke();
      }
   }
}