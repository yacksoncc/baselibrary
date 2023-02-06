using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Extensions
{
   public static class ExtensionTransform
   {
      public static float DistanceToTransform(this Transform argTransform, Transform argTransformObjective)
      {
         return Vector3.Distance(argTransform.position, argTransformObjective.position);
      }

      public static float DistanceToPosition(this Transform argTransform, Vector3 argPositionObjective)
      {
         return Vector3.Distance(argTransform.position, argPositionObjective);
      }

      public static float DistanceXZToTransform(this Transform argTransform, Transform argTransformObjective)
      {
         var tmpPosition = argTransform.position;
         var tmpPositionObjective = argTransformObjective.position;
         return Vector3.Distance(new Vector3(tmpPosition[0], 0, tmpPosition[2]), new Vector3(tmpPositionObjective[0], 0, tmpPositionObjective[2]));
      }

      public static float DistanceXZToPosition(this Transform argTransform, Vector3 argPositionObjective)
      {
         var tmpPosition = argTransform.position;
         return Vector3.Distance(new Vector3(tmpPosition[0], 0, tmpPosition[2]), new Vector3(argPositionObjective[0], 0, argPositionObjective[2]));
      }

      public static Vector3 DirectionToTransform(this Transform argTransform, Transform argTransformObjective)
      {
         return argTransformObjective.position - argTransform.position;
      }
      
      public static Vector3 DirectionToPosition(this Transform argTransform, Vector3 argPositionObjective)
      {
         return argPositionObjective - argTransform.position;
      }

      public static Vector3 DirectionToTransformNormalized(this Transform argTransform, Transform argTransformObjective)
      {
         return (argTransformObjective.position - argTransform.position).normalized;
      }

      public static Vector3 DirectionToPositionNormalized(this Transform argTransform, Vector3 argPositionObjective)
      {
         return (argPositionObjective - argTransform.position).normalized;
      }

      public static Vector3 DirectionXZToTransform(this Transform argTransform, Transform argTransformObjective)
      {
         var tmpDirection = argTransformObjective.position - argTransform.position;
         tmpDirection[1] = 0f;
         return tmpDirection;
      }

      public static Vector3 DirectionXZToPosition(this Transform argTransform, Vector3 argPositionObjective)
      {
         var tmpDirection = argPositionObjective - argTransform.position;
         tmpDirection[1] = 0f;
         return tmpDirection;
      }

      public static Vector3 DirectionXZToTransformNormalized(this Transform argTransform, Transform argTransformObjective)
      {
         var tmpDirection = argTransformObjective.position - argTransform.position;
         tmpDirection[1] = 0f;
         return tmpDirection.normalized;
      }

      public static Vector3 DirectionXZToPositionNormalized(this Transform argTransform, Vector3 argPositionObjective)
      {
         var tmpDirection = argPositionObjective - argTransform.position;
         tmpDirection[1] = 0f;
         return tmpDirection.normalized;
      }

      public static void CleanGameObjectsChildrens(this Transform argTransform, int argBeginCleanFromChildrenIndex = 0, bool argCleanInmediatly = true)
      {
         while(argTransform.childCount > argBeginCleanFromChildrenIndex)
         {
            var tmpGameObjectChildren = argTransform.GetChild(argBeginCleanFromChildrenIndex).gameObject;

            if(argCleanInmediatly)
               GameObject.DestroyImmediate(tmpGameObjectChildren);
            else
               GameObject.Destroy(tmpGameObjectChildren);
         }
      }

      public static List<T> CreatePrefabsHowChildrensAndGetHisComponents<T>(this Transform argTransform, GameObject argPrefab, int argQuantity = 1, UnityAction<GameObject, int> argCallBackWithGameObjectCreated = null, UnityAction<T, int> argCallBackWithComponentOfGameObjectCreated = null)
      {
         var tmpListComponents = new List<T>();

         for(int i = 0; i < argQuantity; i++)
         {
            var tmpNewGameObject = GameObject.Instantiate(argPrefab, argTransform);
            var tmpComponent = tmpNewGameObject.GetComponent<T>();
            tmpListComponents.Add(tmpComponent);
            argCallBackWithGameObjectCreated?.Invoke(tmpNewGameObject, i);
            argCallBackWithComponentOfGameObjectCreated?.Invoke(tmpComponent, i);
         }

         return tmpListComponents;
      }

      public static List<T> CreatePrefabsHowChildrensAndGetHisComponents<T>(this Transform argTransform, GameObject argPrefab, int argQuantity = 1, UnityAction<T, int> argCallBackWithComponentOfGameObjectCreated = null)
      {
         var tmpListComponent = new List<T>();

         for(int i = 0; i < argQuantity; i++)
         {
            var tmpNewGameObject = GameObject.Instantiate(argPrefab, argTransform);
            var tmpComponent = tmpNewGameObject.GetComponent<T>();
            argCallBackWithComponentOfGameObjectCreated?.Invoke(tmpComponent, i);
            tmpListComponent.Add(tmpComponent);
         }

         return tmpListComponent;
      }
   }
}