#pragma warning disable 0660
#pragma warning disable 0661
using UnityEngine;

namespace NSScriptableValues
{
    [CreateAssetMenu(fileName = "ScriptableValueVector3", menuName = "NSScriptableValue/ScriptableValueVector3", order = 0)]
    public class ScriptableValueVector3 : AbstractScriptableValue<Vector3>
    {
        #region sobre carga operadores
        
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
        #endregion
    }
}