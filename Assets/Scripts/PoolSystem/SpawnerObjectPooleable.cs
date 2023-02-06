using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PoolSystem
{
   public class SpawnerObjectPooleable : MonoBehaviour
   {
      [SerializeField]
      private GameObject prefabObjectPooleable;

      [SerializeField]
      private float intervalForSpawnEachObjectInSeconds;

      [SerializeField]
      private int maxQuantityObjectsCanBeSpawned;

      [SerializeField]
      private Bounds boundsForSpawnIn;

      private void Start()
      {
         StartCoroutine(CouSpawn());
      }

      private void OnDrawGizmos()
      {
         Gizmos.DrawWireCube(transform.position, boundsForSpawnIn.extents * 2);
      }

      private IEnumerator CouSpawn()
      {
         if(maxQuantityObjectsCanBeSpawned > 0)
         {
            var tmpActualQuantityObjectsSpawned = 0;
            
            while(tmpActualQuantityObjectsSpawned < maxQuantityObjectsCanBeSpawned)
            {
               var tmpRandomPosition = new Vector3(Random.Range(-boundsForSpawnIn.extents[0], boundsForSpawnIn.extents[0]), Random.Range(-boundsForSpawnIn.extents[1], boundsForSpawnIn.extents[1]), Random.Range(-boundsForSpawnIn.extents[2], boundsForSpawnIn.extents[2]));
               Pool.Instance.InstantiateGameObjectPooleable(prefabObjectPooleable, transform.position + tmpRandomPosition, Quaternion.identity);
               tmpActualQuantityObjectsSpawned++;
               yield return new WaitForSeconds(intervalForSpawnEachObjectInSeconds);
            }
         }
         else
            while(true)
            {
               var tmpRandomPosition = new Vector3(Random.Range(-boundsForSpawnIn.extents[0], boundsForSpawnIn.extents[0]), Random.Range(-boundsForSpawnIn.extents[1], boundsForSpawnIn.extents[1]), Random.Range(-boundsForSpawnIn.extents[2], boundsForSpawnIn.extents[2]));
               Pool.Instance.InstantiateGameObjectPooleable(prefabObjectPooleable, transform.position + tmpRandomPosition, Quaternion.identity);
               yield return new WaitForSeconds(intervalForSpawnEachObjectInSeconds);
            }
      }
   }
}