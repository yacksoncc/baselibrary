#pragma warning disable
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
   public abstract class ScriptableEvent<T> : ScriptableObject
   {
      [Tooltip("Show debug code?")]
      [SerializeField]
      private bool showDebug;

      protected UnityEventT<T> actionEvent = new UnityEventT<T>();

      private string scriptableEventName;

      private T lastValue;

      [SerializeField]
      private bool notifyLastValueToLastSubcripter;

#if UNITY_EDITOR
      private void OnEnable()
      {
         var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
         scriptableEventName = Path.GetFileNameWithoutExtension(assetPath);
      }
#endif

      public void Subscribe(UnityAction<T> argMethodSubscribe)
      {
         actionEvent.AddListener(argMethodSubscribe);

         if(notifyLastValueToLastSubcripter)
            argMethodSubscribe.Invoke(lastValue);

         if(showDebug)
         {
            Debug.Log("actionEvent : " + actionEvent.GetType());
            Debug.Log("Subscribe Method : " + argMethodSubscribe.Method.Name + " To scriptable event :" + scriptableEventName == string.Empty? name : scriptableEventName);
         }
      }

      public void Unsubscribe(UnityAction<T> argMethodUnsubscribe)
      {
         actionEvent.RemoveListener(argMethodUnsubscribe);

         if(showDebug)
            Debug.Log("Unsubscribe Method : " + argMethodUnsubscribe.Method.Name + " To scriptable event :" + scriptableEventName == string.Empty? name : scriptableEventName);
      }

      public void ExecuteEvent(T argValue)
      {
         actionEvent.Invoke(argValue);
         lastValue = argValue;

         if(showDebug)
            Debug.Log("Execute event : " + scriptableEventName == string.Empty? name : scriptableEventName);
      }
   }
}