using UnityEngine;

namespace Extensions
{
   public static class ExtensionDistances
   {
      public static T GetComponentMoreNearToPositionXZ<T>(Vector3 argPosition, T[] arrayComponentMoreNear) where T : Component
      {
         var tmpMaxDistance = float.MaxValue;
         var tmpComponentMoreNearToPosition = default(T);

         foreach(var tmpComponent in arrayComponentMoreNear)
         {
            var tmpDistanceToComponent = tmpComponent.transform.DistanceXZToPosition(argPosition);
            if(tmpDistanceToComponent < tmpMaxDistance)
            {
               tmpComponentMoreNearToPosition = tmpComponent;
               tmpMaxDistance = tmpDistanceToComponent;
            }
         }

         return tmpComponentMoreNearToPosition;
      }
      
      public static GameObject GetGameObjectMoreNearToPositionXZ(Vector3 argPosition, GameObject[] arrayGameObjectMoreNear)
      {
         var tmpMaxDistance = float.MaxValue;
         GameObject tmpGameObjectMoreNearToPosition = null;

         foreach(var tmpGameObject in arrayGameObjectMoreNear)
         {
            var tmpDistanceToComponent = tmpGameObject.transform.DistanceXZToPosition(argPosition);
            
            if(tmpDistanceToComponent < tmpMaxDistance)
            {
               tmpGameObjectMoreNearToPosition = tmpGameObject;
               tmpMaxDistance = tmpDistanceToComponent;
            }
         }

         return tmpGameObjectMoreNearToPosition;
      }
   }
}