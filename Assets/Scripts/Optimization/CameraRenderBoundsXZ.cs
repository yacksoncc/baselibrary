using ScriptableEvents;
using UnityEngine;

namespace Optimization
{
   public class CameraRenderBoundsXZ : MonoBehaviour
   {
      [Header("Setup")]
      [SerializeField]
      private Camera refCameraToRender;

      [SerializeField]
      private float thresholdToUpdateValues = 0.4f;

      [SerializeField]
      private Transform transformPlaneFloor;

      private Plane planeGround;

      private Bounds boundsRender;

      private Vector2 previousPositionCameraXZ;

      private readonly Vector3 expandBoundsAmount = new Vector3(1, 0, 1);

      private float previousPositionCameraY;

      [Header("Events")]
      [SerializeField]
      private ScriptableEventEmpty seBoundsRenderUpdated;

      private bool boundsWasUpdatedTheLastFrame;

      public Bounds BoundsRender
         => boundsRender;

      private void OnEnable()
      {
         var tmpPositionPlaneFloor = transformPlaneFloor.position;
         boundsRender = new Bounds(tmpPositionPlaneFloor, Vector3.one);
         planeGround = new Plane(transformPlaneFloor.up, tmpPositionPlaneFloor);
      }

      private void Update()
      {
         UpdateBoundsCenter();
      }

      private void UpdateBoundsCenter()
      {
         var tmpCameraPositionXYZ = refCameraToRender.transform.position;
         var tmpPositionXZ = new Vector2(tmpCameraPositionXYZ[0], tmpCameraPositionXYZ[2]);
         var tmpPreviousPositionCameraY = tmpPositionXZ[1];

         if((tmpPositionXZ - previousPositionCameraXZ).magnitude > thresholdToUpdateValues)
         {
            previousPositionCameraXZ = tmpPositionXZ;
            boundsWasUpdatedTheLastFrame = true;

            var tmpRayFromCenterScreen = refCameraToRender.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.width * 0.5f));
            planeGround.Raycast(tmpRayFromCenterScreen, out var tmpDistanceToPlane);
            var tmpPositionCenter = tmpRayFromCenterScreen.GetPoint(tmpDistanceToPlane);
            tmpPositionCenter[0] = Mathf.Floor(tmpPositionCenter[0]);
            tmpPositionCenter[2] = Mathf.Floor(tmpPositionCenter[2]);
            boundsRender.center = new Vector3(tmpPositionCenter[0], 0, tmpPositionCenter[2]);
         }

         if(Mathf.Abs(previousPositionCameraY - tmpPreviousPositionCameraY) > thresholdToUpdateValues)
         {
            previousPositionCameraY = tmpPreviousPositionCameraY;
            boundsWasUpdatedTheLastFrame = true;
            boundsRender.size = Vector3.one;

            var tmpBounds0_0Ray = refCameraToRender.ScreenPointToRay(new Vector3(0, 0));
            planeGround.Raycast(tmpBounds0_0Ray, out var tmpDistanceEnter);
            boundsRender.Encapsulate(tmpBounds0_0Ray.GetPoint(tmpDistanceEnter));

            var tmpBounds1_0Ray = refCameraToRender.ScreenPointToRay(new Vector3(Screen.width, 0));
            planeGround.Raycast(tmpBounds1_0Ray, out tmpDistanceEnter);
            boundsRender.Encapsulate(tmpBounds1_0Ray.GetPoint(tmpDistanceEnter));

            var tmpBounds1_1Ray = refCameraToRender.ScreenPointToRay(new Vector3(Screen.width, Screen.height));
            planeGround.Raycast(tmpBounds1_1Ray, out tmpDistanceEnter);
            boundsRender.Encapsulate(tmpBounds1_1Ray.GetPoint(tmpDistanceEnter));

            var tmpBounds0_1Ray = refCameraToRender.ScreenPointToRay(new Vector3(0, Screen.height));
            planeGround.Raycast(tmpBounds0_1Ray, out tmpDistanceEnter);
            boundsRender.Encapsulate(tmpBounds0_1Ray.GetPoint(tmpDistanceEnter));
            boundsRender.Expand(expandBoundsAmount);
         }

         if(boundsWasUpdatedTheLastFrame)
            seBoundsRenderUpdated.ExecuteEvent();
      }

      public bool GetIfWorldPositionCanBeRendered(Vector3 argWorldPosition)
      {
         return boundsRender.Contains(argWorldPosition);
      }
   }
}