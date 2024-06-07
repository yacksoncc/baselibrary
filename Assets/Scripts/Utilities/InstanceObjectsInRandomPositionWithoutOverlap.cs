using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
   public class InstanceObjectsInRandomPositionWithoutOverlap : MonoBehaviour
   {
      private readonly List<Transform> listTransform = new List<Transform>(20);

      private void Awake()
      {
         FillListTransformPoints();
      }

      private void FillListTransformPoints()
      {
         if(listTransform.Count > 0)
            return;

         foreach(Transform tmpChild in transform)//agrega por orden 
            listTransform.Add(tmpChild);
      }

      public void ForceFillListTransformPoints()
      {
         listTransform.Clear();

         foreach(Transform tmpChild in transform)
            listTransform.Add(tmpChild);
      }

      public T InstantiateObject<T>(T argObjectToIntance, Quaternion argRotation) where T : UnityEngine.Object// ejecuta 
      {
         FillListTransformPoints();
         var tmpRandomIndex = Random.Range(0, listTransform.Count);
         var tmpTransformPointToInstantiate = listTransform[tmpRandomIndex];
         listTransform.RemoveAt(tmpRandomIndex);
         return Instantiate(argObjectToIntance, tmpTransformPointToInstantiate.position, argRotation);
      }

#if UNITY_EDITOR

      [SerializeField]
      private float radiusVisualization = 0.2f;

      private void OnDrawGizmos()
      {
         foreach(Transform tmpChild in transform)
            Gizmos.DrawWireSphere(tmpChild.position, radiusVisualization);
      }
#endif
   }
}