using UnityEngine;

namespace RandomPoints
{
   public class RandomPointInSphere : RandomPoint
   {
      [SerializeField]
      private bool useAxisY;

      [SerializeField]
      private float radius;

      private void OnDrawGizmos()
      {
         Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
         Gizmos.DrawWireSphere(Vector3.zero, radius);
         Gizmos.color = Color.white;
         Gizmos.DrawSphere(transform.worldToLocalMatrix.MultiplyPoint3x4(GetRamdomPoint()), 0.3f);
      }

      public override Vector3 GetRamdomPoint()
      {
         var tmpPositionRandom = Random.insideUnitSphere;

         if(!useAxisY)
            tmpPositionRandom[1] = 0;

         return transform.TransformPoint(tmpPositionRandom * radius);
      }
   }
}