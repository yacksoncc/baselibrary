using UnityEngine;

namespace ScriptableEvents
{
   [CreateAssetMenu(fileName = "seByte", menuName = "ScriptableEvents/ScriptableEventByte", order = 0)]
   public class ScriptableEventByte : ScriptableEvent<byte>
   {
      public override void ExecuteEvent(byte argValue)
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