using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "seBool", menuName = "ScriptableEvents/ScriptableEventBool", order = 0)]
    public class ScriptableEventBool : ScriptableEvent<bool>
    {
        public override void ExecuteEvent(bool argValue)
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