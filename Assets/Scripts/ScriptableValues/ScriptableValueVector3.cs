using UnityEngine;

namespace ScriptableValues
{
    [CreateAssetMenu(fileName = "svVector3", menuName = "ScriptableValue/Vector3", order = 0)]
    public class ScriptableValueVector3 : ScriptableValue<Vector3>
    {
        public static implicit operator ScriptableValueVector3(Vector3 argScriptableValueA)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueVector3>();
            tmpNewScriptableValue.Value = argScriptableValueA;
            return tmpNewScriptableValue;
        }
        
        public static implicit operator Vector3(ScriptableValueVector3 argScriptableValueA)
        {
            return argScriptableValueA.value;
        }
    }
}