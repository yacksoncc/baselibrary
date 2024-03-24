using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace PoolSystem
{
   [Serializable]
   public class ObjectPoolWrapper
   {
      public GameObject goPool;

      public int initPooledQuantity;

      private Collection<IObjectPooleable> listObjectsPooleables = new Collection<IObjectPooleable>();

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
         var tmpNewObjectPool = GameObject.Instantiate(goPool);
         var tmpIObjectPooleable = tmpNewObjectPool.GetComponent<IObjectPooleable>();

         if(tmpIObjectPooleable == null)
            Debug.LogError($"The prefab {goPool.name} cant be pooled because has no IObjectPooleable implementation");
         else
         {
            tmpIObjectPooleable.InstantiateFromPoolFirstTime(Vector3.zero, tmpNewObjectPool.transform.rotation, null, true);
            listObjectsPooleables.Add(tmpIObjectPooleable);
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