#pragma warning disable
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
   public abstract class ScriptableEvent<T> : ScriptableObject
   {
      private readonly UnityEventT<T> actionEvent = new UnityEventT<T>();

      private T lastValue;

      [SerializeField]
      private bool notifyLastValueToLastSubcripter;

#if UNITY_EDITOR
      [Tooltip("Show debug code?")]
      [SerializeField]
      private bool showDebug;

      private string scriptableEventName;

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

#if UNITY_EDITOR
         if(showDebug)
         {
            Debug.Log("actionEvent : " + actionEvent.GetType());
            Debug.Log("Subscribe Method : " + argMethodSubscribe.Method.Name + " To scriptable event :" + scriptableEventName == string.Empty? name : scriptableEventName);
         }
#endif
      }

      public void Unsubscribe(UnityAction<T> argMethodUnsubscribe)
      {
         actionEvent.RemoveListener(argMethodUnsubscribe);

#if UNITY_EDITOR
         if(showDebug)
            Debug.Log("Unsubscribe Method : " + argMethodUnsubscribe.Method.Name + " To scriptable event :" + scriptableEventName == string.Empty? name : scriptableEventName);
#endif
      }

      public void ExecuteEvent(T argValue)
      {
         actionEvent.Invoke(argValue);
         lastValue = argValue;

#if UNITY_EDITOR
         if(showDebug)
            Debug.Log("Execute event : " + scriptableEventName == string.Empty? name : scriptableEventName);
#endif
      }
   }
}