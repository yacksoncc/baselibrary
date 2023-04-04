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

      public TimerCounter(float argTime)
      {
         time = argTime;
      }

      public void Reset()
      {
         actualTime = time;
      }

      public void FixedUpdate(float argValue = -1f)
      {
         actualTime += argValue * Time.fixedDeltaTime;
      }

      public void Update(float argValue = -1f)
      {
         actualTime = argValue + Time.deltaTime;
      }

      public bool CheckIfTimeIsZeroFixedUpdate(float argValue = -1f)
      {
         actualTime += argValue * Time.fixedDeltaTime;
         return actualTime <= 0;
      }

      public bool CheckIfTimeIsZeroUpdate(float argValue = -1f)
      {
         actualTime += argValue * Time.deltaTime;
         return actualTime <= 0;
      }

      public bool CheckIfTimeIsZeroAndResetFinishFixedUpdate(float argValue = -1f)
      {
         actualTime += argValue * Time.fixedDeltaTime;

         if(actualTime <= 0)
         {
            Reset();
            return true;
         }

         return false;
      }

      public bool CheckIfTimeIsZeroAndResetFinishUpdate(float argValue = -1f)
      {
         actualTime += argValue * Time.deltaTime;

         if(actualTime <= 0)
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

      public float GetFactor()
      {
         return actualTime / time;
      }
   }
}