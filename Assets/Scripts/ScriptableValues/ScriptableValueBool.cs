using UnityEngine;

namespace NSScriptableValues
{
    [CreateAssetMenu(fileName = "ScriptableValueBool", menuName = "NSScriptableValue/ScriptableValueBool", order = 0)]
    public class ScriptableValueBool : AbstractScriptableValue<bool>
    {
        #region sobre carga operadores

        public static implicit operator ScriptableValueBool(bool argScriptableValueA)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueBool>();
            tmpNewScriptableValue.Value = argScriptableValueA;
            return tmpNewScriptableValue;
        }

        public static implicit operator bool(ScriptableValueBool argScriptableValueA)
        {
            return argScriptableValueA.value;
        }
        #endregion
    }
}