using UnityEngine;

namespace ScriptableValues
{
   [CreateAssetMenu(fileName = "svUShort", menuName = "ScriptableValue/UShort", order = 0)]
   public class ScriptableValueUShort : ScriptableValue<ushort>
   {
      public static implicit operator ScriptableValueUShort(ushort argScriptableValueA)
      {
         var tmpNewScriptableValue = CreateInstance<ScriptableValueUShort>();
         tmpNewScriptableValue.Value = argScriptableValueA;
         return tmpNewScriptableValue;
      }

      public static implicit operator ushort(ScriptableValueUShort argScriptableValueA)
      {
         return argScriptableValueA.value;
      }
   }
}