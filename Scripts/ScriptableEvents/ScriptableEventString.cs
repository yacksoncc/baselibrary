using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "seString", menuName = "ScriptableEvents/ScriptableEventString", order = 0)]
    public class ScriptableEventString : ScriptableEvent<string>
    {
        public override void ExecuteEvent(string argValue)
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