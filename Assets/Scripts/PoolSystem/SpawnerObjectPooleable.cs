using System.Collections;
using RandomPoints;
using UnityEngine;

namespace PoolSystem
{
   public class SpawnerObjectPooleable : MonoBehaviour
   {
      [SerializeField]
      private GameObject prefabObjectPooleable;

      [SerializeField]
      private float intervalForSpawnEachObjectInSeconds = 1;

      [SerializeField]
      private int maxQuantityObjectsCanBeSpawned;

      [SerializeField]
      private RandomPoint randomPointForSpawnIn; // todo implement bounds random

      private void Start()
      {
         StartCoroutine(CouSpawn());
      }

      private IEnumerator CouSpawn()
      {
         if(maxQuantityObjectsCanBeSpawned > 0)
         {
            var tmpActualQuantityObjectsSpawned = 0;

            while(tmpActualQuantityObjectsSpawned < maxQuantityObjectsCanBeSpawned)
            {
               Pool.Instance.InstantiateGameObjectPooleable(prefabObjectPooleable, randomPointForSpawnIn.GetRamdomPoint(), Quaternion.identity);
               tmpActualQuantityObjectsSpawned++;
               yield return new WaitForSeconds(intervalForSpawnEachObjectInSeconds);
            }
         }
         else
            while(true)
            {
               Pool.Instance.InstantiateGameObjectPooleable(prefabObjectPooleable, randomPointForSpawnIn.GetRamdomPoint(), Quaternion.identity);
               yield return new WaitForSeconds(intervalForSpawnEachObjectInSeconds);
            }
      }
   }
}