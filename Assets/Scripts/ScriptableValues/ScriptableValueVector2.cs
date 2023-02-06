#pragma warning disable 0660
#pragma warning disable 0661
using UnityEngine;


namespace NSScriptableValues
{
    [CreateAssetMenu(fileName = "ScriptableValueVector2", menuName = "NSScriptableValue/ScriptableValueVector2", order = 0)]
    public class ScriptableValueVector2 : AbstractScriptableValue<Vector2>
    {
        #region sobre carga operadores
        
        public static implicit operator ScriptableValueVector2(Vector2 argScriptableValueA)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueVector2>();
            tmpNewScriptableValue.Value = argScriptableValueA;
            return tmpNewScriptableValue;
        }
        
        public static implicit operator Vector2(ScriptableValueVector2 argScriptableValueA)
        {
            return argScriptableValueA.value;
        }
        #endregion
    }
}