using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Splines
{
   [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
   public class SplineObject : MonoBehaviour
   {
      [SerializeField]
      private SplineMesh refSplineMesh;

      [SerializeField]
      private float distanceOnSpline;

      private float previousDistanceOnSpline;

      [SerializeField]
      private float sidePosition;

      private float previousSidePosition;

      public UnityEvent OnMeshUpdated;

      private MeshFilter refRenderer;

      private Vector3[] arrayVertex;

      public float DistanceOnSpline
      {
         set => distanceOnSpline = value;
      }

      public float SidePosition
      {
         set => sidePosition = value;
      }

      private void Awake()
      {
         refRenderer = GetComponent<MeshFilter>();
         arrayVertex = refRenderer.mesh.vertices;
      }

      private void Update()
      {
         if(Mathf.Abs(distanceOnSpline - previousDistanceOnSpline) > 0.001 || Mathf.Abs(sidePosition - previousSidePosition) > 0.001)
         {
            RebuilMesh();
            previousDistanceOnSpline = distanceOnSpline;
            previousSidePosition = sidePosition;
         }
      }

      private void RebuilMesh()
      {
         transform.position = refSplineMesh.GetPositionAtDistance(distanceOnSpline) + refSplineMesh.GetOrientationAtDistance(distanceOnSpline) * Vector3.right * sidePosition;
         var tmpVertices = new List<Vector3>(arrayVertex);

         for(int tmpIndexVertexToModify = 0; tmpIndexVertexToModify < tmpVertices.Count; tmpIndexVertexToModify++)
         {
            var tmpVertexPosition = tmpVertices[tmpIndexVertexToModify];
            var tmpPositionOnPath = refSplineMesh.GetPositionAtDistance(distanceOnSpline + tmpVertexPosition[2]);
            var tmpRotationVertex = refSplineMesh.GetOrientationAtDistance(distanceOnSpline + tmpVertexPosition[2]);
            tmpVertexPosition[2] = 0;
            tmpVertices[tmpIndexVertexToModify] = transform.InverseTransformPoint(tmpPositionOnPath + tmpRotationVertex * tmpVertexPosition + tmpRotationVertex * Vector3.right * sidePosition);
         }

         refRenderer.mesh.SetVertices(tmpVertices);
         refRenderer.mesh.RecalculateBounds();
         OnMeshUpdated.Invoke();
      }
   }
}