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
      private Transform transformPlaneFloor;

      [SerializeField]
      private float thresholdToUpdateValues = 0.4f;

      [Header("Raycast-based mode")]
      [SerializeField]
      private Vector3 expandBoundsAmount = new(1, 0, 1);

      [Header("Frustum-cone mode")]
      [SerializeField]
      private float frustumDistance = 80f;

      private readonly Vector2[] frustumVertsXZ = new Vector2[3];

      private Plane planeGround;

      private Bounds boundsRender;

      private Vector2 previousPositionCameraXZ;

      private float previousYaw;

      private bool boundsWasUpdatedTheLastFrame;

      [Header("Events")]
      [SerializeField]
      private ScriptableEventEmpty seBoundsRenderUpdated;

      public bool RenderModeFrustumCulling { get; set; } = false;

      public Bounds BoundsRender
         => boundsRender;

      void OnEnable()
      {
         var tmpPosPlane = transformPlaneFloor.position;
         boundsRender = new Bounds(tmpPosPlane, Vector3.one);
         planeGround = new Plane(transformPlaneFloor.up, tmpPosPlane);
         previousYaw = refCameraToRender.transform.eulerAngles.y;
      }

      void Update()
         => UpdateBoundsCenter();

      void UpdateBoundsCenter()
      {
         var tmpCamPos = refCameraToRender.transform.position;
         var tmpPosXZ = new Vector2(tmpCamPos.x, tmpCamPos.z);
         var tmpYaw = refCameraToRender.transform.eulerAngles.y;

         var tmpPosDelta = (tmpPosXZ - previousPositionCameraXZ).magnitude;
         var tmpYawDelta = Mathf.Abs(Mathf.DeltaAngle(tmpYaw, previousYaw));

         if(tmpPosDelta <= thresholdToUpdateValues && tmpYawDelta <= 0.5f)
            return;

         previousPositionCameraXZ = tmpPosXZ;
         previousYaw = tmpYaw;
         boundsWasUpdatedTheLastFrame = true;

         if(RenderModeFrustumCulling)
         {
            var tmpApexXZ = new Vector2(tmpCamPos.x, tmpCamPos.z);
            var tmpForwardXZ = new Vector2(refCameraToRender.transform.forward.x, refCameraToRender.transform.forward.z).normalized;

            var tmpHorizontalFOV = 2f * Mathf.Atan(Mathf.Tan(refCameraToRender.fieldOfView * Mathf.Deg2Rad * 0.5f) * refCameraToRender.aspect) * Mathf.Rad2Deg;

            var tmpHalfAng = tmpHorizontalFOV * 0.5f;

            var tmpDirLeft = Quaternion.Euler(0f, -tmpHalfAng, 0f) * new Vector3(tmpForwardXZ.x, 0f, tmpForwardXZ.y);
            var tmpDirRight = Quaternion.Euler(0f, tmpHalfAng, 0f) * new Vector3(tmpForwardXZ.x, 0f, tmpForwardXZ.y);

            var tmpLeftPt = tmpApexXZ + new Vector2(tmpDirLeft.x, tmpDirLeft.z) * frustumDistance;
            var tmpRightPt = tmpApexXZ + new Vector2(tmpDirRight.x, tmpDirRight.z) * frustumDistance;

            frustumVertsXZ[0] = tmpApexXZ;
            frustumVertsXZ[1] = tmpLeftPt;
            frustumVertsXZ[2] = tmpRightPt;

            boundsRender.center = new Vector3(tmpApexXZ.x, 0f, tmpApexXZ.y);
            boundsRender.size = Vector3.one;
            boundsRender.Encapsulate(new Vector3(tmpLeftPt.x, 0f, tmpLeftPt.y));
            boundsRender.Encapsulate(new Vector3(tmpRightPt.x, 0f, tmpRightPt.y));
         }
         else
         {
            var tmpRay = refCameraToRender.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.width * 0.5f));
            planeGround.Raycast(tmpRay, out var tmpDistToPlane);
            var tmpCenter = tmpRay.GetPoint(tmpDistToPlane);
            boundsRender.center = new Vector3(tmpCenter.x, 0f, tmpCenter.z);
            boundsRender.size = Vector3.one;

            var tmpCorners = new Vector3[]
                             {
                                new(0, 0), new(Screen.width, 0), new(Screen.width, Screen.height), new(0, Screen.height)
                             };

            foreach(var tmpCorner in tmpCorners)
            {
               var tmpRayCorner = refCameraToRender.ScreenPointToRay(tmpCorner);
               planeGround.Raycast(tmpRayCorner, out var tmpDist);
               boundsRender.Encapsulate(tmpRayCorner.GetPoint(tmpDist));
            }

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
         if(RenderModeFrustumCulling)
         {
            // AABB rápida
            if(!boundsRender.Contains(argWorldPosition))
               return false;
            
            var tmpP = new Vector2(argWorldPosition.x, argWorldPosition.z);
            var tmpA = frustumVertsXZ[0];
            var tmpB = frustumVertsXZ[1];
            var tmpC = frustumVertsXZ[2];

            bool SameSide(Vector2 argP1, Vector2 argP2, Vector2 argA, Vector2 argB)
            {
               var tmpCross1 = (argB - argA).x * (argP1.y - argA.y) - (argB - argA).y * (argP1.x - argA.x);
               var tmpCross2 = (argB - argA).x * (argP2.y - argA.y) - (argB - argA).y * (argP2.x - argA.x);
               return tmpCross1 * tmpCross2 >= 0f;
            }

            return SameSide(tmpP, tmpA, tmpB, tmpC) && SameSide(tmpP, tmpB, tmpA, tmpC) && SameSide(tmpP, tmpC, tmpA, tmpB);
         }

         return boundsRender.Contains(argWorldPosition);
      }

#if UNITY_EDITOR
      void OnDrawGizmos()
      {
         Gizmos.color = Color.yellow;
         Gizmos.DrawCube(boundsRender.center, boundsRender.size);

         if(RenderModeFrustumCulling && frustumVertsXZ[1] != Vector2.zero)
         {
            Gizmos.color = Color.cyan;
            var tmpY = 0f;
            Gizmos.DrawLine(new Vector3(frustumVertsXZ[0].x, tmpY, frustumVertsXZ[0].y), new Vector3(frustumVertsXZ[1].x, tmpY, frustumVertsXZ[1].y));
            Gizmos.DrawLine(new Vector3(frustumVertsXZ[0].x, tmpY, frustumVertsXZ[0].y), new Vector3(frustumVertsXZ[2].x, tmpY, frustumVertsXZ[2].y));
            Gizmos.DrawLine(new Vector3(frustumVertsXZ[1].x, tmpY, frustumVertsXZ[1].y), new Vector3(frustumVertsXZ[2].x, tmpY, frustumVertsXZ[2].y));
         }
      }
#endif
   }
}