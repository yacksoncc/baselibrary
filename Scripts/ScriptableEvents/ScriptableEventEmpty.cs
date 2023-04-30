using System.IO;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableEvents//cambiar el name space
{
   [CreateAssetMenu(fileName = "se", menuName = "ScriptableEvents/ScriptableEventEmpty", order = 0)]
   public class ScriptableEventEmpty : ScriptableObject
   {
      [SerializeField]
      protected UnityEvent actionEvent;

      [SerializeField]
      private bool showDebug;

      private string scriptableEventName;

#if UNITY_EDITOR
      private void OnEnable()
      {
         var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
         scriptableEventName = Path.GetFileNameWithoutExtension(assetPath);
      }
#endif

      public void Subscribe(UnityAction argMethodSubscribe)
      {
         actionEvent.AddListener(argMethodSubscribe);

         if(showDebug)
            Debug.Log("Subscribe Method : " + argMethodSubscribe.Method.Name + " To scriptable event :" + scriptableEventName == string.Empty? name : scriptableEventName);
      }

      public void Unsubscribe(UnityAction argMethodUnsubscribe)
      {
         actionEvent.RemoveListener(argMethodUnsubscribe);

         if(showDebug)
            Debug.Log("Unsubscribe Method : " + argMethodUnsubscribe.Method.Name + " To scriptable event :" + scriptableEventName == string.Empty? name : scriptableEventName);
      }

      public void ExecuteEvent()
      {
         actionEvent.Invoke();

         if(showDebug)
            Debug.Log("Execute event : " + scriptableEventName == string.Empty? name : scriptableEventName);
      }
   }

   public class UnityEventT<T> : UnityEvent<T>
   {
   }
}