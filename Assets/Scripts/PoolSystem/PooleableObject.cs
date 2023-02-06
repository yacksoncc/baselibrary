using Patterns.Observer;
using UnityEngine;

namespace PoolSystem
{
   public class PooleableObject : AbstractSubject, IObjectPooleable
   {
      [Tooltip("automatic gameobject enable or disable when it is created or destroyed?")]
      [SerializeField]
      private bool autoEnableDisableGameObject;

      public bool Destroyed { get; set; }

      public void InstantiateFromPool(Vector3 argPosition, Quaternion argRotation, Transform argParent = null)
      {
         transform.SetPositionAndRotation(argPosition, argRotation);
         transform.SetParent(argParent);
         Destroyed = false;

         if(autoEnableDisableGameObject)
            gameObject.SetActive(true);

         NotifyToObservers(PooleableUnitNotyfications.InstantiateFromPool);
      }

      /// <summary>
      /// Instantiate first time from pool
      /// </summary>
      /// <param name="argInitDefault">For know if was instantiated for predict pool size or by user</param>
      public void InstantiateFromPoolFirstTime(Vector3 argPosition, Quaternion argRotation, Transform argParent = null, bool argInitDefault = false)
      {
         transform.SetPositionAndRotation(argPosition, argRotation);
         transform.SetParent(argParent);

         if(argInitDefault)
         {
            Destroyed = true;
            NotifyToObservers(PooleableUnitNotyfications.InstantiateFromPoolInitDefault);
         }
         else
         {
            if(autoEnableDisableGameObject)
               gameObject.SetActive(true);

            Destroyed = false;
            NotifyToObservers(PooleableUnitNotyfications.InstantiateFromPoolFirstTime);
         }
      }

      public void DestroyPooleableObject()
      {
         if(autoEnableDisableGameObject)
            gameObject.SetActive(false);
         
         Destroyed = true;
         NotifyToObservers(PooleableUnitNotyfications.Destroyed);
      }

      public enum PooleableUnitNotyfications
      {
         InstantiateFromPool,
         InstantiateFromPoolFirstTime,
         Destroyed,
         InstantiateFromPoolInitDefault
      }
   }
}