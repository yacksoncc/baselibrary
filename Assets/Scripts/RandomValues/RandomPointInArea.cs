using UnityEngine;

namespace RandomValues
{
   public class RandomPointInArea : MonoBehaviour
   {
      [SerializeField]
      private AreaType areaType = AreaType.Box;

      [SerializeField]
      private bool useAxisY;

      [SerializeField]
      private Bounds boundAreaTypeBox;

      [SerializeField]
      private float radius;

      private void OnDrawGizmos()
      {
         Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

         switch(areaType)
         {
            case AreaType.Box:
               Gizmos.DrawWireCube(Vector3.zero, boundAreaTypeBox.size);
               break;

            case AreaType.Sphere:
               Gizmos.DrawWireSphere(Vector3.zero, radius);
               break;
         }

         Gizmos.color = Color.red;
         Gizmos.DrawSphere(transform.worldToLocalMatrix.MultiplyPoint3x4(GetRamdomPoint()), 0.3f);
      }

      public Vector3 GetRamdomPoint()
      {
         switch(areaType)
         {
            case AreaType.Box:
               return transform.TransformPoint(new Vector3(Random.Range(-boundAreaTypeBox.extents[0], boundAreaTypeBox.extents[0]), useAxisY? Random.Range(-boundAreaTypeBox.extents[1], boundAreaTypeBox.extents[1]) : 0, Random.Range(-boundAreaTypeBox.extents[2], boundAreaTypeBox.extents[2])));

            case AreaType.Sphere:
               var tmpPositionRandom = Random.insideUnitSphere;

               if(!useAxisY)
                  tmpPositionRandom[1] = 0;

               return transform.TransformPoint(tmpPositionRandom * radius);
         }

         return Vector3.zero;
      }
   }

   public enum AreaType
   {
      Box,
      Sphere
   }
}