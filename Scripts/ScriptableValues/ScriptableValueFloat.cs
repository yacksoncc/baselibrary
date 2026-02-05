#pragma warning disable 0660
#pragma warning disable 0661
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableValues
{
    [CreateAssetMenu(fileName = "svFloat", menuName = "ScriptableValue/Float", order = 0)]
    public class ScriptableValueFloat : ScriptableValue<float>
    {

    }
}