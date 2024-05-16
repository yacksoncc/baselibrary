using UnityEngine;

namespace ScriptableEvents
{
   [CreateAssetMenu(fileName = "seUshort", menuName = "ScriptableEvents/ScriptableEventUshort", order = 0)]
   public class ScriptableEventUshort : ScriptableEvent<ushort>
   {
      public override void ExecuteEvent(ushort argValue)
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