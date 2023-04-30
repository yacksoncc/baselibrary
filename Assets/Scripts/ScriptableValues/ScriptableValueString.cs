using UnityEngine;

namespace ScriptableValues
{
    [CreateAssetMenu(fileName = "svString", menuName = "ScriptableValue/String", order = 0)]
    public class ScriptableValueString : ScriptableValue<string>
    {
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
    }
}