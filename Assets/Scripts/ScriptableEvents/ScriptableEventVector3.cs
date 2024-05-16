using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "seVector3", menuName = "ScriptableEvents/ScriptableEventVector3", order = 0)]
    public class ScriptableEventVector3 : ScriptableEvent<Vector3>
    {
        public override void ExecuteEvent(Vector3 argValue)
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