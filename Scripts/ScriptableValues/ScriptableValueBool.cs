using UnityEngine;

namespace ScriptableValues
{
    [CreateAssetMenu(fileName = "svBool", menuName = "ScriptableValue/Bool", order = 0)]
    public class ScriptableValueBool : AbstractScriptableValue<bool>
    {
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
    }
}