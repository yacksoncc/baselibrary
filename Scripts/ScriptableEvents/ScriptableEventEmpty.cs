using System.IO;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableEvents
{
   [CreateAssetMenu(fileName = "se", menuName = "ScriptableEvents/ScriptableEventEmpty", order = 0)]
   public class ScriptableEventEmpty : ScriptableObject
   {
      [SerializeField]
      protected UnityEvent actionEvent;

#if UNITY_EDITOR
      [SerializeField]
      private bool showDebug;

      private string scriptableEventName;

      private void OnEnable()
      {
         var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
         scriptableEventName = Path.GetFileNameWithoutExtension(assetPath);
      }
#endif

      public void Subscribe(UnityAction argMethodSubscribe)
      {
         actionEvent.AddListener(argMethodSubscribe);

#if UNITY_EDITOR
         if(showDebug)
            Debug.Log("Subscribe Method : " + argMethodSubscribe.Method.Name + " To scriptable event :" + scriptableEventName == string.Empty? name : scriptableEventName);
#endif
      }

      public void Unsubscribe(UnityAction argMethodUnsubscribe)
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

      public void ExecuteEvent()
      {
         actionEvent.Invoke();

#if UNITY_EDITOR
         if(showDebug)
            Debug.Log("Execute event : " + scriptableEventName == string.Empty? name : scriptableEventName);
#endif
      }
   }

   public class UnityEventT<T> : UnityEvent<T>
   {
   }
}