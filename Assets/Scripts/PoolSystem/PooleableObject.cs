using Patterns.Observer;
using UnityEngine;

namespace PoolSystem
{
   public class PooleableObject : Subject, IObjectPooleable
   {
      public bool Destroyed { get; set; }

      public void InstantiateFromPool(Vector3 argPosition, Quaternion argRotation, Transform argParent = null)
      {
         transform.SetPositionAndRotation(argPosition, argRotation);
         transform.SetParent(argParent);
         Destroyed = false;
         gameObject.SetActive(true);
         NotifyToObservers(PooleableUnitNotyfications.InstantiateFromPool);
      }
      
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
            gameObject.SetActive(true);
            Destroyed = false;
            NotifyToObservers(PooleableUnitNotyfications.InstantiateFromPoolFirstTime);
         }
      }

      public void DestroyPooleableObject()
      {
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