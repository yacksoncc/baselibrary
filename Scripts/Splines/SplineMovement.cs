using UnityEngine;

namespace Splines
{
   public class SplineMovement : MonoBehaviour
   {
      [SerializeField]
      private float speed;

      private float distanceTraveled;

      [SerializeField]
      private SplineMesh refSplineMesh;

      public float Speed
      {
         get => speed;
         set => speed = value;
      }

      public float DistanceTraveled
      {
         get => distanceTraveled;
         set => distanceTraveled = value;
      }

      private void Update()
      {
         distanceTraveled += speed * Time.deltaTime;
         transform.position = refSplineMesh.GetPositionAtDistance(distanceTraveled);
         transform.rotation = refSplineMesh.GetOrientationAtDistance(distanceTraveled);
      }
   }
}