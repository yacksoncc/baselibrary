using Unity.Mathematics;
using UnityEngine;

namespace RandomPoints
{
   [ExecuteInEditMode]
   public class PerlinNoise : MonoBehaviour
   {
      [SerializeField]
      private float resolution = 1;

      [SerializeField]
      private bool WorldSpace;

      private float valueXOffset;

      private float valueZOffset;

      [SerializeField]
      private float animationValueXOffsetSpeed;

      [SerializeField]
      private float animationValueZOffsetSpeed;

      [Header("Debug")]
      [SerializeField]
      private bool enableDebug;

      [SerializeField, Range(0.25f, 1)]
      private float cellDensity = 0.5f;

      [SerializeField]
      private Bounds boundsPerlinNoise;

      private void Update()
      {
         valueXOffset += animationValueXOffsetSpeed * Time.deltaTime;
         valueZOffset += animationValueZOffsetSpeed * Time.deltaTime;
      }

      private void OnDrawGizmos()
      {
         if(!enableDebug)
            return;

         Gizmos.matrix = transform.localToWorldMatrix;
         Gizmos.color = Color.red;
         Gizmos.DrawWireCube(new float3(0, 1, 0) * boundsPerlinNoise.extents[1], boundsPerlinNoise.size);

         var tmpQuantityCellsX = math.floor(boundsPerlinNoise.size[0] / cellDensity);
         var tmpQuantityCellsZ = math.floor(boundsPerlinNoise.size[2] / cellDensity);
         var tmpCellLenghtX = boundsPerlinNoise.size[0] / tmpQuantityCellsX;
         var tmpCellLenghtZ = boundsPerlinNoise.size[2] / tmpQuantityCellsZ;

         for(int x = 0; x < tmpQuantityCellsX; x++)
            for(int z = 0; z < tmpQuantityCellsZ; z++)
            {
               DrawLineXCell(x * tmpCellLenghtX - boundsPerlinNoise.extents[0], z * tmpCellLenghtZ - boundsPerlinNoise.extents[2], tmpCellLenghtX);
               DrawLineZCell(x * tmpCellLenghtX - boundsPerlinNoise.extents[0], z * tmpCellLenghtZ - boundsPerlinNoise.extents[2], tmpCellLenghtZ);
            }

         var tmpInitPosition = new float3(0, EvaluatePerlinNoise(0, 0, WorldSpace), 0);
         var tmpNormal = EvaluateNormalPerlinNoise(0, 0, WorldSpace);
         Gizmos.color = Color.green;
         Gizmos.DrawLine(tmpInitPosition, tmpInitPosition + tmpNormal);
      }

      /*private void DrawCell(float argPosX, float argPosZ)
      {
         var tmpPointLeftBottom = new Vector3(argPosX, EvaluatePerlinNoise(argPosX, argPosZ), argPosZ);
         var tmpPointLeftTop = new Vector3(argPosX, EvaluatePerlinNoise(argPosX, argPosZ + cellSize), argPosZ + cellSize);
         var tmpPointRightTop = new Vector3(argPosX + cellSize, EvaluatePerlinNoise(argPosX + cellSize, argPosZ + cellSize), argPosZ + cellSize);
         var tmpPointRightBottom = new Vector3(argPosX + cellSize, EvaluatePerlinNoise(argPosX + cellSize, argPosZ), argPosZ);
         
         Gizmos.color = Color.Lerp(Color.black, Color.white, tmpPointLeftBottom[1]+ 0.5f);
         Gizmos.DrawLine(tmpPointLeftBottom, tmpPointLeftTop);
         Gizmos.color = Color.Lerp(Color.black, Color.white, tmpPointLeftTop[1]  + 0.5f);
         Gizmos.DrawLine(tmpPointLeftTop, tmpPointRightTop);
         Gizmos.color = Color.Lerp(Color.black, Color.white, tmpPointRightTop[1] + 0.5f);
         Gizmos.DrawLine(tmpPointRightTop, tmpPointRightBottom);
         Gizmos.color = Color.Lerp(Color.black, Color.white, tmpPointRightBottom[1] + 0.5f);
         Gizmos.DrawLine(tmpPointRightBottom, tmpPointLeftBottom);
      }*/

      private void DrawLineXCell(float argPosX, float argPosZ, float argCellLenghtX)
      {
         var tmpPointLeftBottom = new float3(argPosX, EvaluatePerlinNoise(argPosX, argPosZ, WorldSpace), argPosZ);
         var tmpPointRightBottom = new float3(argPosX + argCellLenghtX, EvaluatePerlinNoise(argPosX + argCellLenghtX, argPosZ, WorldSpace), argPosZ);
         Gizmos.color = Color.Lerp(Color.black, Color.white, tmpPointRightBottom[1] / boundsPerlinNoise.size[1]);
         Gizmos.DrawLine(tmpPointLeftBottom, tmpPointRightBottom);
      }

      private void DrawLineZCell(float argPosX, float argPosZ, float argCellLenghtZ)
      {
         var tmpPointLeftBottom = new float3(argPosX, EvaluatePerlinNoise(argPosX, argPosZ, WorldSpace), argPosZ);
         var tmpPointLeftTop = new float3(argPosX, EvaluatePerlinNoise(argPosX, argPosZ + argCellLenghtZ, WorldSpace), argPosZ + argCellLenghtZ);
         Gizmos.color = Color.Lerp(Color.black, Color.white, tmpPointLeftBottom[1] / boundsPerlinNoise.size[1]);
         Gizmos.DrawLine(tmpPointLeftBottom, tmpPointLeftTop);
      }

      public float EvaluatePerlinNoise(float argPosX, float argPosZ, bool argTransformFromLocalToWorld = false)
      {
         Vector3 tmpPosition;

         if(argTransformFromLocalToWorld)
            tmpPosition = transform.TransformPoint(new float3(argPosX, 0, argPosZ)) / resolution;
         else
            tmpPosition = (transform.position + new Vector3(argPosX, 0, argPosZ)) / resolution;

         tmpPosition += new Vector3(valueXOffset, 0, valueZOffset);
         return (Mathf.PerlinNoise(tmpPosition[0], tmpPosition[2])) * boundsPerlinNoise.size[1];
      }

      public float3 EvaluateNormalPerlinNoise(float argPosX, float argPosZ, bool argTransformFromLocalToWorld = false)
      {
         var tmpPrecision = 0.005f;
         var tmpInitVector = new float3(0, EvaluatePerlinNoise(argPosX, argPosZ, argTransformFromLocalToWorld), 0);
         var tmpVectorX = new float3(tmpPrecision, EvaluatePerlinNoise(argPosX + tmpPrecision, argPosZ, argTransformFromLocalToWorld), 0) - tmpInitVector;
         var tmpVectorZ = new float3(0, EvaluatePerlinNoise(argPosX, argPosZ + tmpPrecision, argTransformFromLocalToWorld), tmpPrecision) - tmpInitVector;
         return math.normalize(math.cross(tmpVectorZ, tmpVectorX));
      }
   }
}