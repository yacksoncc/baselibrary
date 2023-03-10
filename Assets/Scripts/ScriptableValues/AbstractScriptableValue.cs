using System;
using UnityEngine;
using UnityEngine.Events;

namespace NSScriptableValues
{
    public abstract class AbstractScriptableValue<T> : ScriptableObject
    {
        [SerializeField] protected bool debug;

        [SerializeField] protected bool notifyValueChanged;

        [SerializeField] protected T value;

        protected T previusValue;

        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;

                if (notifyValueChanged)
                    if (!this.value.Equals(previusValue))
                    {
                        previusValue = this.value;
                        OnValueChanged.Invoke();
                    }

                if (debug)
                    Debug.Log("Scriptable Value " + name + " : " + value);
            }
        }
        
        public UnityEvent OnValueChanged;
    }
}