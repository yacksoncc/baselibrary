using UnityEngine;

namespace ScriptableValues
{
   [CreateAssetMenu(fileName = "svShort", menuName = "ScriptableValue/Short", order = 0)]
   public class ScriptableValueShort : ScriptableValue<short>
   {
      public static implicit operator ScriptableValueShort(short argScriptableValueA)
      {
         var tmpNewScriptableValue = CreateInstance<ScriptableValueShort>();
         tmpNewScriptableValue.Value = argScriptableValueA;
         return tmpNewScriptableValue;
      }

      public static implicit operator short(ScriptableValueShort argScriptableValueA)
      {
         return argScriptableValueA.value;
      }
   }
}