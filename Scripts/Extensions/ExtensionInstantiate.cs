using UnityEngine;
using UnityEngine.Events;

namespace Extensions
{
   public static class ExtensionInstantiate
   {
      public static void InstantiateWithComponent<T>(GameObject argPrefab, UnityAction<T> argCallbackWithComponentOfGameObjectInstantied, Transform argTransformParent = null, Vector3 argPosition = default(Vector3), Quaternion argRotation = default(Quaternion)) where T : Component
      {
         var tmpNewGameObject = GameObject.Instantiate(argPrefab, argPosition, argRotation, argTransformParent);
         var tmpComponent = tmpNewGameObject.AddComponent<T>();
         argCallbackWithComponentOfGameObjectInstantied?.Invoke(tmpComponent);
      }
   }
}