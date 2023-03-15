#pragma warning disable 0660
#pragma warning disable 0661
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableValues
{
    [CreateAssetMenu(fileName = "svFloat", menuName = "ScriptableValue/Float", order = 0)]
    public class ScriptableValueFloat : AbstractScriptableValue<float>
    {
/*
        //initialization
        public static implicit operator ScriptableValueFloat(float argScriptableValueA)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueFloat>();
            tmpNewScriptableValue.Value = argScriptableValueA;
            return tmpNewScriptableValue;
        }
        
        public static implicit operator float(ScriptableValueFloat argScriptableValueA)
        {
            return argScriptableValueA.value;
        }

        //operations self
        
        public static ScriptableValueFloat operator -(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueFloat>();
            tmpNewScriptableValue.Value = argScriptableValueA.value - argScriptableValueB.value;
            return tmpNewScriptableValue;
        }
        
        public static ScriptableValueFloat operator +(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueFloat>();
            tmpNewScriptableValue.Value = argScriptableValueA.value + argScriptableValueB.value;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueFloat operator *(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueFloat>();
            tmpNewScriptableValue.Value = argScriptableValueA.value * argScriptableValueB.value;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueFloat operator /(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueFloat>();
            tmpNewScriptableValue.Value = argScriptableValueA.value / argScriptableValueB.value;
            return tmpNewScriptableValue;
        }

        //operations
        
        public static ScriptableValueFloat operator -(ScriptableValueFloat argScriptableValueA, float argValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueFloat>();
            tmpNewScriptableValue.Value = argScriptableValueA.value - argValueB;
            return tmpNewScriptableValue;
        }
        
        public static ScriptableValueFloat operator +(ScriptableValueFloat argScriptableValueA, float argValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueFloat>();
            tmpNewScriptableValue.Value = argScriptableValueA.value + argValueB;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueFloat operator *(ScriptableValueFloat argScriptableValueA, float argValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueFloat>();
            tmpNewScriptableValue.Value = argScriptableValueA.value * argValueB;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueFloat operator /(ScriptableValueFloat argScriptableValueA, float argValueB)
        {
            var tmpNewScriptableValue = CreateInstance<ScriptableValueFloat>();
            tmpNewScriptableValue.Value = argScriptableValueA.value / argValueB;
            return tmpNewScriptableValue;
        }

        public static ScriptableValueFloat operator ++(ScriptableValueFloat argScriptableValueA)
        {
            argScriptableValueA.Value++;
            return argScriptableValueA;
        }

        public static ScriptableValueFloat operator --(ScriptableValueFloat argScriptableValueA)
        {
            argScriptableValueA.Value--;
            return argScriptableValueA;
        }*/

        //comparisions
        
        public static bool operator >(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            return argScriptableValueA.value > argScriptableValueB.value;
        }

        public static bool operator <(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            return argScriptableValueA.value < argScriptableValueB.value;
        }

        public static bool operator ==(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            return argScriptableValueA.value == argScriptableValueB.value;
        }

        public static bool operator !=(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            return argScriptableValueA.value != argScriptableValueB.value;
        }

        public static bool operator >=(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            return argScriptableValueA.value >= argScriptableValueB.value;
        }

        public static bool operator <=(ScriptableValueFloat argScriptableValueA, ScriptableValueFloat argScriptableValueB)
        {
            return argScriptableValueA.value <= argScriptableValueB.value;
        }
    }
}