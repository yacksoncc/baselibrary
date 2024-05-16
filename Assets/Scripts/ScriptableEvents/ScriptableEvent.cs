#pragma warning disable
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableEvents
{
   public abstract class ScriptableEvent<T> : ScriptableObject
   {
      protected readonly UnityEventT<T> actionEvent = new UnityEventT<T>();

      protected T lastValue;

      [SerializeField]
      private bool notifyLastValueToLastSubcripter;

#if UNITY_EDITOR
      [Tooltip("Show debug code?")]
      [SerializeField]
      protected bool showDebug;

      protected string scriptableEventName;

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

      public void RemoveAllListerners()
      {
         actionEvent.RemoveAllListeners();

#if UNITY_EDITOR
         if(showDebug)
            Debug.Log("Remove all listeners");
#endif
      }

      public abstract void ExecuteEvent(T argValue);
   }
}