using UnityEngine;

namespace ScriptableEvents
{
    [CreateAssetMenu(fileName = "seVector2", menuName = "ScriptableEvents/ScriptableEventVector2", order = 0)]
    public class ScriptableEventVector2 : ScriptableEvent<Vector2>
    {
        public override void ExecuteEvent(Vector2 argValue)
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