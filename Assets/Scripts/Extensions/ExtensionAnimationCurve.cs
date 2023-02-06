using System;
using UnityEngine;

namespace Extensions
{
   public static class ExtensionAnimationCurve
   {
      public static double GetTimeFromValue(this AnimationCurve argAnimationCurve, double argValue)
      {
         double tmpLeftValueOnCurve = argAnimationCurve[0].time;
         double tmpRightValueOnCurve = argAnimationCurve[argAnimationCurve.length - 1].time;
         const ushort tmpIterations = 10;
         const double tmpStepEvaluationResolution = 20d;
         var tmpValueMoreNearToTime = 1d;

         for(ushort i = 0; i < tmpIterations; i++)
         {
            var tmpStepEvaluationSize = (tmpRightValueOnCurve - tmpLeftValueOnCurve) / tmpStepEvaluationResolution;

            for(double tmpEvaluationOnCurve = tmpLeftValueOnCurve; tmpEvaluationOnCurve <= tmpRightValueOnCurve; tmpEvaluationOnCurve += tmpStepEvaluationSize)
            {
               double tmpValueOnCurve = argAnimationCurve.Evaluate(Convert.ToSingle(tmpEvaluationOnCurve));

               if(Mathf.Abs(Convert.ToSingle(tmpValueOnCurve - argValue)) < tmpStepEvaluationSize)
                  tmpValueMoreNearToTime = tmpValueOnCurve;
            }

            tmpLeftValueOnCurve = tmpValueMoreNearToTime - tmpStepEvaluationSize;
            tmpRightValueOnCurve = tmpValueMoreNearToTime + tmpStepEvaluationSize;
         }

         return tmpValueMoreNearToTime;
      }
   }
}