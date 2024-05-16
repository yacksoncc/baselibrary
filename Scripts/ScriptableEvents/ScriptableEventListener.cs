#pragma warning disable 0649
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
   public class ScriptableEventListener : MonoBehaviour
   {
      [SerializeField]
      private ScriptableEventEmpty scriptableEventEmpty;

      [SerializeField]
      private float delayToExecute;

      private WaitForSeconds waitForSecondsDelay;

      public UnityEvent unityEvent;

      private void Awake()
      {
         waitForSecondsDelay = new WaitForSeconds(delayToExecute);
      }

      private void OnEnable()
      {
         if(scriptableEventEmpty)
            scriptableEventEmpty.Subscribe(ExecuteEvent);
      }

      private void OnDisable()
      {
         if(scriptableEventEmpty)
            scriptableEventEmpty.Unsubscribe(ExecuteEvent);
      }

      public void ExecuteEvent()
      {
         if(delayToExecute > 0)
            StartCoroutine(CouExecuteEventDelay());
         else
            unityEvent.Invoke();
      }

      private IEnumerator CouExecuteEventDelay()
      {
         yield return waitForSecondsDelay;
      }
   }
}