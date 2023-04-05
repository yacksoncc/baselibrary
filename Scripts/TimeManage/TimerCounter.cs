using System;
using UnityEngine;

namespace TimeManage
{
   [Serializable]
   public class TimerCounter
   {
      [SerializeField]
      private float time;

      [SerializeField]
      private float actualTime;

      public float ActualTime
      {
         get => actualTime;
         set => actualTime = value;
      }

      public float TimeNormalized
      {
         get => actualTime / time;
      }

      public TimerCounter(float argTime)
      {
         time = argTime;
      }

      public void Reset()
      {
         actualTime = time;
      }

      public void Update(float argValue = -1f)
      {
         actualTime += argValue * Time.deltaTime;
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