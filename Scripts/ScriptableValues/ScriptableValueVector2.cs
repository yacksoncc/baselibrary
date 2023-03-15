using UnityEngine;


namespace ScriptableValues
{
    [CreateAssetMenu(fileName = "svVector2", menuName = "ScriptableValue/Vector2", order = 0)]
    public class ScriptableValueVector2 : AbstractScriptableValue<Vector2>
    {
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
    }
}