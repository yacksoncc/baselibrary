using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "seTransform", menuName = "ScriptableEvents/ScriptableEventTransform", order = 0)]
    public class ScriptableEventTransform : ScriptableEvent<Transform>
    {
        public override void ExecuteEvent(Transform argValue)
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