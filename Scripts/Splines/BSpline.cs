using System;
using System.Collections.Generic;
using UnityEngine;

namespace BezierCurve
{
   [ExecuteInEditMode]
   public class BSpline : MonoBehaviour
   {
      [SerializeField]
      private int orderK = 4;

      [SerializeField]
      private float[] t;

      public List<ContainerKnot> listKnots = new List<ContainerKnot>();

      public Vector3 EvaluateComulativePoint(float argT)
      {
         var tmpPoint = listKnots[0].knotPosition * BAltern(0, 1);

         for(int i = 1; i < listKnots.Count; i++)
            tmpPoint += (listKnots[i].knotPosition - listKnots[i - 1].knotPosition) * BAltern(i, argT);

         return tmpPoint;
      }

      private float BAltern(int i, float argT)
      {
         if(argT > t[i] && argT < t[i + orderK - 1])
         {
            var tmpValue = 0f;

            for(int j = i; j <= i + orderK; j++)
               tmpValue += B(j, orderK, argT);

            return tmpValue;
         }

         if(argT >= t[i + orderK - 1])
            return 1;

         return 0;
      }

      private float B(int i, int k, float argT)
      {
         if(k == 1)
         {
            if(argT > t[i] && argT < t[i + 1])
               return 1;

            return 0;
         }

         var tmpPart0Equation0 = argT - t[i];
         var tmpPart1Equation0 = t[i + k - 1] - t[i];
         var tmpEquation0 = (tmpPart0Equation0 / tmpPart1Equation0) * B(i, k - 1, argT);

         var tmpPart0Equation1 = t[i + k] - argT;
         var tmpPart1Equation1 = t[i + k] - t[i + 1];
         var tmpEquation1 = (tmpPart0Equation1 / tmpPart1Equation1) * B(i + 1, k - 1, argT);
         return tmpEquation0 + tmpEquation1;
      }

      /*
      private static Quaternion ExpQuaternion(Quaternion argQuaternion)
      {
         float vectorMagnitude = Mathf.Sqrt(argQuaternion.x * argQuaternion.x + argQuaternion.y * argQuaternion.y + argQuaternion.z * argQuaternion.z);
         float expW = Mathf.Exp(argQuaternion.w);

         if(Mathf.Abs(vectorMagnitude) < 1e-6)
         {
            // The quaternion is too close to a pure scalar, so return the exp of the scalar part
            expW = Mathf.Exp(argQuaternion.w);
            return new Quaternion(0, 0, 0, expW);
         }

         float cosVectorMagnitude = Mathf.Cos(vectorMagnitude);
         float sinVectorMagnitude = Mathf.Sin(vectorMagnitude);

         float coeff = expW * sinVectorMagnitude / vectorMagnitude;
         return new Quaternion(coeff * argQuaternion.x, coeff * argQuaternion.y, coeff * argQuaternion.z, expW * cosVectorMagnitude);
      }

      public static Quaternion LogQuaternion(Quaternion argQuaternion)
      {
         float magnitude = argQuaternion.magnitude;
         Quaternion qNormalized = argQuaternion.normalized;

         float vectorMagnitude = Mathf.Sqrt(qNormalized.x * qNormalized.x + qNormalized.y * qNormalized.y + qNormalized.z * qNormalized.z);

         if(Mathf.Abs(vectorMagnitude) < 1e-6)
         {
            // The quaternion is too close to a pure scalar, so return the log of the scalar part
            return new Quaternion(0, 0, 0, Mathf.Log(magnitude));
         }

         float angle = Mathf.Acos(qNormalized.w);
         float logMagnitude = Mathf.Log(magnitude);

         float coeff = angle / vectorMagnitude;
         return new Quaternion(coeff * qNormalized.x, coeff * qNormalized.y, coeff * qNormalized.z, logMagnitude);
      }*/
   }

   [Serializable]
   public class ContainerKnot
   {
      public Vector3 knotPosition;

      public Quaternion knotRotation;

      public ContainerKnot(Vector3 argKnotPosition, Quaternion argKnotRotation)
      {
         knotPosition = argKnotPosition;
         knotRotation = argKnotRotation;
      }
   }
}