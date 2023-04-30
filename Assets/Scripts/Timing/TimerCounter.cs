using System;
using UnityEngine;

namespace Timing
{
   [Serializable]
   public class TimerCounter
   {
      [SerializeField]
      private float time;

      [SerializeField]
      private float actualTime;

      [SerializeField]
      private AnimationCurve acTimeEvaluated;

      public float ActualTime
      {
         get => actualTime;
         set => actualTime = value;
      }

      public float TimeNormalized
      {
         get => actualTime / time;
      }

      public float TimeEvaluated
      {
         get => acTimeEvaluated.Evaluate(TimeNormalized);
      }

      public TimerCounter(float argTime = 1)
      {
         time = argTime;
         acTimeEvaluated = AnimationCurve.Linear(0, 0, 1, 1);
      }

      public void Reset()
      {
         actualTime = time;
      }

      public float Update(float argValue = -1f)
      {
         actualTime += argValue * Time.deltaTime;
         return TimeNormalized;
      }

      public bool UpdateAndCheckIfTimeIsZero(float argValue = -1f)
      {
         actualTime += argValue * Time.deltaTime;
         return actualTime <= 0;
      }

      public bool UpdateAndCheckIfTimeIsZeroAndReset(float argValue = -1f)
      {
         actualTime += argValue * Time.deltaTime;

         if(CheckIfTimeIsZero())
         {
            Reset();
            return true;
         }

         return false;
      }

      public bool CheckIfTimeIsZero()
      {
         return actualTime <= 0;
      }
   }
}