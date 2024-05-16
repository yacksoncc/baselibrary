using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "seInt", menuName = "ScriptableEvents/ScriptableEventInt", order = 0)]
    public class ScriptableEventInt : ScriptableEvent<int>
    {
        public override void ExecuteEvent(int argValue)
        {
            actionEvent.Invoke(argValue);
            lastValue = argValue;

#if UNITY_EDITOR
            if(showDebug)
                Debug.Log("Execute event : " + scriptableEventName == string.Empty? name : scriptableEventName);
#endif
        }
    }
}