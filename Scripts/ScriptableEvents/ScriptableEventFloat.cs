using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "seFloat", menuName = "ScriptableEvents/ScriptableEventFloat", order = 0)]
    public class ScriptableEventFloat : ScriptableEvent<float>
    {
        public override void ExecuteEvent(float argValue)
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