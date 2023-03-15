using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
   public static class ExtensionPhysics
   {
      public static List<T> OverlapSphereGetComponent<T>(Vector3 argPosition, float argRadius, LayerMask argLayerMask, GameObject argGameObjectForIgnore = null)
      {
         var tmpArrayCollisionWhitOtherObjects = Physics.OverlapSphere(argPosition, argRadius, argLayerMask);
         var tmpListComponentsFindedWithCollision = new List<T>();

         foreach(var tmpCollider in tmpArrayCollisionWhitOtherObjects)
         {
            if(argGameObjectForIgnore != null && tmpCollider.gameObject == argGameObjectForIgnore)
               continue;

            var tmpComponent = tmpCollider.GetComponent<T>();
            tmpListComponentsFindedWithCollision.Add(tmpComponent);
         }

         return tmpListComponentsFindedWithCollision;
      }

      public static List<T> OverlapBoxGetComponent<T>(Vector3 argPosition, Vector3 argHalfExtensions, Quaternion argRotation, LayerMask argLayerMask, GameObject argGameObjectForIgnore = null)
      {
         var tmpArrayCollisionWhitOtherObjects = Physics.OverlapBox(argPosition, argHalfExtensions, argRotation, argLayerMask);
         var tmpListComponentsFindedWithCollision = new List<T>();

         foreach(var tmpCollider in tmpArrayCollisionWhitOtherObjects)
         {
            if(argGameObjectForIgnore != null && tmpCollider.gameObject == argGameObjectForIgnore)
               continue;

            var tmpComponent = tmpCollider.GetComponent<T>();
            tmpListComponentsFindedWithCollision.Add(tmpComponent);
         }

         return tmpListComponentsFindedWithCollision;
      }
   }
}