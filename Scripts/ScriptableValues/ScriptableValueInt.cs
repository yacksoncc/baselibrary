#pragma warning disable 0660
#pragma warning disable 0661
using UnityEngine;

namespace NSScriptableValues
{
    [CreateAssetMenu(fileName = "ScriptableValueInt", menuName = "NSScriptableValue/Int", order = 0)]
    public class ScriptableValueInt : AbstractScriptableValue<int>
    {
        #region sobre carga operadores
        /*
        //initialization

        public static implicit operator ScriptableValueInt(int argScriptableValueA)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueInt>();
            tmpNewScriptableValue.Value = argScriptableValueA;
            return tmpNewScriptableValue;
        }
        
        public static implicit operator int(ScriptableValueInt argScriptableValueA)
        {
            return argScriptableValueA.value;
        }

        //operations self
        
        public static ScriptableValueInt operator -(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueInt>();
            tmpNewScriptableValue.Value = argScriptableValueA.value - argScriptableValueB.value;
            return tmpNewScriptableValue;
        }
        
        public static ScriptableValueInt operator +(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueInt>();
            tmpNewScriptableValue.Value = argScriptableValueA.value + argScriptableValueB.value;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueInt operator *(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueInt>();
            tmpNewScriptableValue.Value = argScriptableValueA.value * argScriptableValueB.value;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueInt operator /(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueInt>();
            tmpNewScriptableValue.Value = argScriptableValueA.value / argScriptableValueB.value;
            return tmpNewScriptableValue;
        }

        //operations
        
        public static ScriptableValueInt operator -(ScriptableValueInt argScriptableValueA, int argValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueInt>();
            tmpNewScriptableValue.Value = argScriptableValueA.value - argValueB;
            return tmpNewScriptableValue;
        }
        
        public static ScriptableValueInt operator +(ScriptableValueInt argScriptableValueA, int argValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueInt>();
            tmpNewScriptableValue.Value = argScriptableValueA.value + argValueB;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueInt operator *(ScriptableValueInt argScriptableValueA, int argValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueInt>();
            tmpNewScriptableValue.Value = argScriptableValueA.value * argValueB;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueInt operator /(ScriptableValueInt argScriptableValueA, int argValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueInt>();
            tmpNewScriptableValue.Value = argScriptableValueA.value / argValueB;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueInt operator ++(ScriptableValueInt argScriptableValueA)
        {
            argScriptableValueA.Value++;
            return argScriptableValueA;
        }

        public static ScriptableValueInt operator --(ScriptableValueInt argScriptableValueA)
        {
            argScriptableValueA.Value--;
            return argScriptableValueA;
        }
    */
        //comparisions
        
        public static bool operator >(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            return argScriptableValueA.value > argScriptableValueB.value;
        }

        public static bool operator <(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            return argScriptableValueA.value < argScriptableValueB.value;
        }

        public static bool operator ==(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            return argScriptableValueA.value == argScriptableValueB.value;
        }

        public static bool operator !=(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            return argScriptableValueA.value != argScriptableValueB.value;
        }

        public static bool operator >=(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            return argScriptableValueA.value >= argScriptableValueB.value;
        }

        public static bool operator <=(ScriptableValueInt argScriptableValueA, ScriptableValueInt argScriptableValueB)
        {
            return argScriptableValueA.value <= argScriptableValueB.value;
        }
        #endregion
    }
}