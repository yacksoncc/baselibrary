using UnityEngine;

namespace NSScriptableValues
{
    [CreateAssetMenu(fileName = "ScriptableValueString", menuName = "NSScriptableValue/ScriptableValueString", order = 0)]
    public class ScriptableValueString : AbstractScriptableValue<string>
    {
        #region sobre carga operadores
        //initialization

        public static implicit operator ScriptableValueString(string argScriptableValueA)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueString>();
            tmpNewScriptableValue.Value = argScriptableValueA;
            return tmpNewScriptableValue;
        }
        
        public static implicit operator string(ScriptableValueString argScriptableValueA)
        {
            return argScriptableValueA.value;
        }
        #endregion
    }
}