using UnityEngine;

namespace ScriptableValues
{
   [CreateAssetMenu(fileName = "svTransform", menuName = "ScriptableValue/Transform", order = 0)]
   public class ScriptableValueTransform : AbstractScriptableValue<Transform>
   {
      public static implicit operator ScriptableValueTransform(Transform argScriptableValueA)
      {
         var tmpNewScriptableValue = CreateInstance<ScriptableValueTransform>();
         tmpNewScriptableValue.Value = argScriptableValueA;
         return tmpNewScriptableValue;
      }

      public static implicit operator Transform(ScriptableValueTransform argScriptableValueA)
      {
         return argScriptableValueA.value;
      }
   }
}