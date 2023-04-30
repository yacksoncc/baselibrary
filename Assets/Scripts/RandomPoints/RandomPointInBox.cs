using UnityEngine;

namespace RandomPoints
{
   public class RandomPointInBox : RandomPoint
   {
      [SerializeField]
      private bool useAxisY;

      [SerializeField]
      private Bounds boundAreaTypeBox;

      private void OnDrawGizmos()
      {
         Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
         Gizmos.DrawWireCube(Vector3.zero, boundAreaTypeBox.size);
         Gizmos.color = Color.white;
         Gizmos.DrawSphere(transform.worldToLocalMatrix.MultiplyPoint3x4(GetRamdomPoint()), 0.3f);
      }

      public override Vector3 GetRamdomPoint()
      {
         var tmpPositionX = Random.Range(-boundAreaTypeBox.extents[0], boundAreaTypeBox.extents[0]);
         var tmpPositionY = useAxisY? Random.Range(-boundAreaTypeBox.extents[1], boundAreaTypeBox.extents[1]) : 0;
         var tmpPositionZ = Random.Range(-boundAreaTypeBox.extents[2], boundAreaTypeBox.extents[2]);
         return transform.TransformPoint(new Vector3(tmpPositionX, tmpPositionY, tmpPositionZ));
      }
   }
}