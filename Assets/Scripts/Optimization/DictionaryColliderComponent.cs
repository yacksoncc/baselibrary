using System.Collections.Generic;
using Extensions;
using ScriptableEvents;
using UnityEngine;

namespace Optimization
{
   public abstract class DictionaryColliderComponent<T> : MonoBehaviour
   {
      [SerializeField]
      private ScriptableEvent<int> svComponentAdded;

      [SerializeField]
      private ScriptableEvent<int> svComponentRemoved;

      private readonly Dictionary<int, T> dictionaryCollider_Component = new Dictionary<int, T>();

      public bool AddCollider(Collider argCollider, T argComponent)
      {
         var tmpInstanceID = argCollider.GetInstanceID();

         if(dictionaryCollider_Component.TryAdd(tmpInstanceID, argComponent))
         {
            svComponentAdded.ExecuteEvent(tmpInstanceID);
            return true;
         }

         return false;
      }

      public bool GetComponent(Collider argCollider, out T argComponent)
      {
         return dictionaryCollider_Component.TryGetValue(argCollider.GetInstanceID(), out argComponent);
      }

      public bool RemoveCollider(Collider argCollider)
      {
         var tmpInstanceID = argCollider.GetInstanceID();

         if(dictionaryCollider_Component.ContainsKey(tmpInstanceID))
         {
            svComponentRemoved.ExecuteEvent(tmpInstanceID);
            dictionaryCollider_Component.Remove(tmpInstanceID);
            return true;
         }

         return false;
      }

      public bool GetComponentMoreNearToPosition(Vector3 argPosition, float argRadius, LayerMask argLayerMaskToTestColliders, out T argComponent)
      {
         var tmpArrayCollidersAroundPosition = Physics.OverlapSphere(argPosition, argRadius, argLayerMaskToTestColliders);

         if(tmpArrayCollidersAroundPosition.Length > 0)
         {
            var tmpCollider = ExtensionDistances.GetComponentMoreNearToPositionXZ(argPosition, tmpArrayCollidersAroundPosition);
            return dictionaryCollider_Component.TryGetValue(tmpCollider.GetInstanceID(), out argComponent);
         }

         argComponent = default(T);
         return false;
      }

      public bool GetComponentsAroundPosition(Vector3 argPosition, float argRadius, LayerMask argLayerMaskToTestColliders, List<T> argListComponents)
      {
         var tmpArrayCollidersAroundPosition = Physics.OverlapSphere(argPosition, argRadius, argLayerMaskToTestColliders);
         var tmpComponentFinded = false;

         foreach(var tmpCollider in tmpArrayCollidersAroundPosition)
            if(dictionaryCollider_Component.TryGetValue(tmpCollider.GetInstanceID(), out var tmpComponent))
            {
               argListComponents.Add(tmpComponent);
               tmpComponentFinded = true;
            }

         return tmpComponentFinded;
      }
   }
}