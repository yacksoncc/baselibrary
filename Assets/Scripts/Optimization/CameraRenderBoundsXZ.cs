using ScriptableEvents;
using Singleton;
using UnityEngine;

namespace Optimization
{
   public class CameraRenderBoundsXZ : Singleton<CameraRenderBoundsXZ>
   {
      [Header("Setup")]
      [SerializeField]
      private Camera refCameraToRender;
   
      [SerializeField]
      private float thresholdToUpdateValues = 0.4f;
   
      [SerializeField]
      private Transform transformPlaneFloor;
   
      [Header("Raycast-based mode")]
      [SerializeField]
      private Vector3 expandBoundsAmount = new (1, 0, 1);
   
      [Header("Radius-based mode")]
      [Tooltip("Radio (m) que se encapsula alrededor de la cámara cuando RenderModeBoundsXZ es true")]
      [SerializeField]
      private float radiusAroundCamera = 40;
      
      private Plane planeGround;
      
      private Bounds boundsRender;
      
      private Vector2 previousPositionCameraXZ;
      
      private bool boundsWasUpdatedTheLastFrame;
      
      public Bounds BoundsRender => boundsRender;

      [Header("Events")]
      [SerializeField]
      private ScriptableEventEmpty seBoundsRenderUpdated;
      
      public bool RenderModeAroundBoundsXZ { get; set; } = false;
   
      private void OnEnable()
      {
         var tmpPositionPlaneFloor = transformPlaneFloor.position;
         boundsRender = new Bounds(tmpPositionPlaneFloor, Vector3.one);
         planeGround = new Plane(transformPlaneFloor.up, tmpPositionPlaneFloor);
      }
   
      private void OnDrawGizmos()
      {
         Gizmos.DrawCube(boundsRender.center, boundsRender.size);
      }
   
      private void Update()
      {
         UpdateBoundsCenter();
      }
   
      private void UpdateBoundsCenter()
      {
         var tmpCameraPositionXYZ = refCameraToRender.transform.position;
         var tmpPositionXZ = new Vector2(tmpCameraPositionXYZ.x, tmpCameraPositionXYZ.z);
   
         if((tmpPositionXZ - previousPositionCameraXZ).magnitude <= thresholdToUpdateValues)
            return;
   
         previousPositionCameraXZ = tmpPositionXZ;
         boundsWasUpdatedTheLastFrame = true;
   
         if(RenderModeAroundBoundsXZ) 
         {
            boundsRender.center = new Vector3(tmpCameraPositionXYZ.x, 0f, tmpCameraPositionXYZ.z);
            var tmpDiameter = radiusAroundCamera * 2f;
            boundsRender.size = new Vector3(tmpDiameter, 1f, tmpDiameter);
         }
         else
         {
            var tmpRayFromCenterScreen = refCameraToRender.ScreenPointToRay(
                                            new Vector2(Screen.width * 0.5f, Screen.width * 0.5f));
            planeGround.Raycast(tmpRayFromCenterScreen, out var tmpDistanceToPlane);
            var tmpPositionCenter = tmpRayFromCenterScreen.GetPoint(tmpDistanceToPlane);
            boundsRender.center = new Vector3(tmpPositionCenter.x, 0f, tmpPositionCenter.z);
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
         {
            boundsWasUpdatedTheLastFrame = false;
            seBoundsRenderUpdated.ExecuteEvent();
         }
      }
   
      public bool GetIfWorldPositionCanBeRendered(Vector3 argWorldPosition)
      {
         return boundsRender.Contains(argWorldPosition);
      }
   }
}