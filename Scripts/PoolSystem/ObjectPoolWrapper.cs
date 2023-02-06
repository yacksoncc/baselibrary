using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem
{
   [Serializable]
   public class ObjectPoolWrapper
   {
      public GameObject goPool;

      public int initPooledQuantity;

      private List<IObjectPooleable> listObjectsPooleables = new List<IObjectPooleable>();

      public void Instantiate(Vector3 argPosition, Quaternion argRotation, Transform argParent = null, bool argInitDefault = false)
      {
         foreach(var tmpObjectPooleable in listObjectsPooleables)
            if(tmpObjectPooleable.Destroyed)
            {
               tmpObjectPooleable.InstantiateFromPool(argPosition, argRotation, argParent);
               return;
            }

         var tmpNewObjectPool = GameObject.Instantiate(goPool).GetComponent<IObjectPooleable>();

         if(tmpNewObjectPool == null)
            Debug.LogError($"The prefab {goPool.name} cant be pooled because has no IObjectPooleable implementation");
         else
         {
            tmpNewObjectPool.InstantiateFromPoolFirstTime(argPosition, argRotation, argParent, argInitDefault);
            listObjectsPooleables.Add(tmpNewObjectPool);
         }
      }

      public void InstantiateDefaultInit()
      {
         var tmpNewObjectPool = GameObject.Instantiate(goPool).GetComponent<IObjectPooleable>();

         if(tmpNewObjectPool == null)
            Debug.LogError($"The prefab {goPool.name} cant be pooled because has no IObjectPooleable implementation");
         else
         {
            tmpNewObjectPool.InstantiateFromPoolFirstTime(Vector3.zero, Quaternion.identity, null, true);
            listObjectsPooleables.Add(tmpNewObjectPool);
         }
      }

      public T Instantiate<T>(Vector3 argPosition, Quaternion argRotation, Transform argParent = null, bool argInitDefault = false)
      {
         T tmpComponent;

         foreach(var tmpObjectPooleable in listObjectsPooleables)
            if(tmpObjectPooleable.Destroyed)
            {
               tmpObjectPooleable.InstantiateFromPool(argPosition, argRotation, argParent);
               tmpComponent = tmpObjectPooleable.GetComponent<T>();
               return tmpComponent;
            }

         var tmpNewObjectPool = GameObject.Instantiate(goPool).GetComponent<IObjectPooleable>();

         if(tmpNewObjectPool == null)
            Debug.LogError($"The prefab {goPool.name} cant be pooled because has no IObjectPooleable implementation");
         else
         {
            tmpNewObjectPool.InstantiateFromPoolFirstTime(argPosition, argRotation, argParent, argInitDefault);
            listObjectsPooleables.Add(tmpNewObjectPool);
         }

         if(tmpNewObjectPool != null)
         {
            tmpComponent = tmpNewObjectPool.GetComponent<T>();
            return tmpComponent;
         }

         return default(T);
      }
   }
}