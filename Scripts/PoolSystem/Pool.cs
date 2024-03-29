using System;
using System.Collections.Generic;
using Singleton;
using UnityEngine;
namespace PoolSystem
{
   public class Pool : Singleton<Pool>
   {
      [Tooltip("Set all prefabs of possible game objects that will instantiated on scene")]
      [SerializeField]
      private SOPoolObjects soPoolObjects;

      private readonly Dictionary<GameObject, ObjectPoolWrapper> dictionaryObjectPoolWrapper = new Dictionary<GameObject, ObjectPoolWrapper>();

      private void Awake()
      {
         foreach(var tmpObjectPoolWrapper in soPoolObjects.arrayObjectPoolWrapper)
            dictionaryObjectPoolWrapper.Add(tmpObjectPoolWrapper.goPool, tmpObjectPoolWrapper);
      }

      private void OnDestroy()
      {
         foreach(var tmpObjectPoolWrapper in soPoolObjects.arrayObjectPoolWrapper)
            tmpObjectPoolWrapper.CleanUp();
      }

      private void Start()
      {
         foreach(var tmpObjectPoolWrapper in soPoolObjects.arrayObjectPoolWrapper)
            for(int i = 0; i < tmpObjectPoolWrapper.initPooledQuantity; i++)
               InstantiateGameObjectPooleableDefaultInit(tmpObjectPoolWrapper.goPool);
      }

      public void InstantiateGameObjectPooleable(GameObject argGameObjectPrefab, Vector3 argPosition, Quaternion argRotation, Transform argParent = null)
      {
         dictionaryObjectPoolWrapper[argGameObjectPrefab].Instantiate(argPosition, argRotation, argParent);
      }

      private void InstantiateGameObjectPooleableDefaultInit(GameObject argGameObjectPrefab)
      {
         dictionaryObjectPoolWrapper[argGameObjectPrefab].InstantiateDefaultInit();
      }

      public T InstantiateGameObjectPooleable<T>(GameObject argGameObjectPrefab, Vector3 argPosition, Quaternion argRotation, Transform argParent = null, bool argInitDefault = false)
      {
         return dictionaryObjectPoolWrapper[argGameObjectPrefab].Instantiate<T>(argPosition, argRotation, argParent, argInitDefault);
      }
   }
}