using UnityEngine;

namespace ScriptableValues
{
   [CreateAssetMenu(fileName = "svByte", menuName = "ScriptableValue/Byte", order = 0)]
   public class ScriptableValueByte : ScriptableValue<byte>
   {
      public static implicit operator ScriptableValueByte(byte argScriptableValueA)
      {
         var tmpNewScriptableValue = CreateInstance<ScriptableValueByte>();
         tmpNewScriptableValue.Value = argScriptableValueA;
         return tmpNewScriptableValue;
      }

      public static implicit operator byte(ScriptableValueByte argScriptableValueA)
      {
         return argScriptableValueA.value;
      }
   }
}