using System;
using UnityEngine;
using Unity.Mathematics;

namespace BezierCurve
{
   [Serializable]
   public class BSplineSegment
   {
      public Transform[] arrayPoints = new Transform[4];

      private float timeToEvaluate;

      private float4 matrixTime;

      private float4 matrixTimeTangent;

      private float4 matrixTimeCurvature;

      private float4 matrixRotations;

      private float4x3 matrixPositions;

      private float4x4 matrixCoeficients = float4x4.zero;

      public BSplineSegment(Transform argTransformPoint0, Transform argTransformPoint1, Transform argTransformPoint2, Transform argTransformPoint3)
      {
         matrixCoeficients.c0 = new float4(1, -3, 3, -1);
         matrixCoeficients.c1 = new float4(4, 0, -6, 3);
         matrixCoeficients.c2 = new float4(1, 3, 3, -3);
         matrixCoeficients.c3 = new float4(0, 0, 0, 1);

         arrayPoints[0] = argTransformPoint0;
         arrayPoints[1] = argTransformPoint1;
         arrayPoints[2] = argTransformPoint2;
         arrayPoints[3] = argTransformPoint3;
      }

      public Vector3 Evaluate(float argT)
      {
         timeToEvaluate = argT;
         SetupTimeFloat4();
         SetupMatrixPositions();
         return math.mul(math.mul(matrixTime, matrixCoeficients), matrixPositions);
         /*
var tmpOneMinusTime = 1f - argT;
var tmpEvaluationTime = tmpOneMinusTime * tmpOneMinusTime * tmpOneMinusTime * arrayPoints[0].position;
tmpEvaluationTime += 3 * tmpOneMinusTime * tmpOneMinusTime * argT * arrayPoints[1].position;
tmpEvaluationTime += 3 * tmpOneMinusTime * argT * argT * arrayPoints[2].position;
tmpEvaluationTime += argT * argT * argT * arrayPoints[3].position;*/
      }

      public float3 EvaluateTangent(float argT)
      {
         timeToEvaluate = argT;
         SetupTimeTangentFloat4();
         SetupMatrixPositions();
         return math.mul(math.mul(matrixTimeTangent, matrixCoeficients), matrixPositions);
      }

      public Vector3 EvaluateNormal(float argT)
      {
         var tmpTangent = EvaluateTangent(argT);
         var tmpRotationP0 = Quaternion.AngleAxis(arrayPoints[0].eulerAngles[2], tmpTangent);
         var tmpRotationP1 = Quaternion.AngleAxis(arrayPoints[1].eulerAngles[2], tmpTangent);
         var tmpRotationP2 = Quaternion.AngleAxis(arrayPoints[2].eulerAngles[2], tmpTangent);
         var tmpRotationP3 = Quaternion.AngleAxis(arrayPoints[3].eulerAngles[2], tmpTangent);

         Quaternion Q0Q1 = Quaternion.SlerpUnclamped(tmpRotationP0, tmpRotationP1, argT + 1);
         Quaternion Q1Q2 = Quaternion.SlerpUnclamped(tmpRotationP1, tmpRotationP2, argT);
         Quaternion Q2Q3 = Quaternion.SlerpUnclamped(tmpRotationP2, tmpRotationP3, argT - 1);

         Quaternion Q0Q1_Q1Q2 = Quaternion.SlerpUnclamped(Q0Q1, Q1Q2, 0.5f * (argT + 1));
         Quaternion Q1Q2_Q2Q3 = Quaternion.SlerpUnclamped(Q1Q2, Q2Q3, 0.5f * argT);

         Quaternion result = Quaternion.SlerpUnclamped(Q0Q1_Q1Q2, Q1Q2_Q2Q3, argT);
         return result * Vector3.up;
      }

      private void SetupTimeFloat4()
      {
         matrixTime[0] = 1;
         matrixTime[1] = timeToEvaluate;
         matrixTime[2] = timeToEvaluate * timeToEvaluate;
         matrixTime[3] = timeToEvaluate * timeToEvaluate * timeToEvaluate;
         matrixTime /= 6f;
      }

      private void SetupTimeTangentFloat4()
      {
         matrixTimeTangent[0] = 0;
         matrixTimeTangent[1] = 1;
         matrixTimeTangent[2] = 2 * timeToEvaluate;
         matrixTimeTangent[3] = 3 * timeToEvaluate * timeToEvaluate;
         matrixTimeTangent /= 6f;
      }

      private void SetupTimeRotationsFloat4()
      {
         matrixRotations[0] = arrayPoints[0].eulerAngles[2] % 360f;
         matrixRotations[1] = arrayPoints[1].eulerAngles[2] % 360f;
         matrixRotations[2] = arrayPoints[2].eulerAngles[2] % 360f;
         matrixRotations[3] = arrayPoints[3].eulerAngles[2] % 360f;
      }

      private void SetupTimeVelocityFloat4()
      {
         matrixTimeCurvature[0] = 0;
         matrixTimeCurvature[1] = 0;
         matrixTimeCurvature[2] = 2;
         matrixTimeCurvature[3] = 6 * timeToEvaluate;
         matrixTimeCurvature /= 6f;
      }

      private void SetupMatrixPositions()
      {
         matrixPositions.c0 = new float4(arrayPoints[0].position[0], arrayPoints[1].position[0], arrayPoints[2].position[0], arrayPoints[3].position[0]);
         matrixPositions.c1 = new float4(arrayPoints[0].position[1], arrayPoints[1].position[1], arrayPoints[2].position[1], arrayPoints[3].position[1]);
         matrixPositions.c2 = new float4(arrayPoints[0].position[2], arrayPoints[1].position[2], arrayPoints[2].position[2], arrayPoints[3].position[2]);
      }
      
      /*
      public Vector3 EvaluateTangent(float argT)
      {
         var tmpOneMinusTime = 1f - argT;
         var tmpEvaluationTangent = tmpOneMinusTime * tmpOneMinusTime * (arrayPoints[1].position - arrayPoints[0].position);
         tmpEvaluationTangent += 2 * tmpOneMinusTime * argT * (arrayPoints[2].position - arrayPoints[1].position);
         tmpEvaluationTangent += argT * argT * (arrayPoints[3].position - arrayPoints[2].position);
         tmpEvaluationTangent *= 3;
         return tmpEvaluationTangent;
      }

      public Vector3 EvaluateNormal(float argT)
      {
         var tmpOneMinusTime = 1f - argT;
         var tmpEvaluationNormal = tmpOneMinusTime * (arrayPoints[2].position - 2 * arrayPoints[1].position + arrayPoints[0].position);
         tmpEvaluationNormal += argT * (arrayPoints[3].position - 2 * arrayPoints[2].position + arrayPoints[1].position);
         tmpEvaluationNormal *= 6;

         var tmpNormal = math.cross(tmpEvaluationNormal, EvaluateTangent(argT));

         return tmpNormal;
      }*/
   }
   
   
}