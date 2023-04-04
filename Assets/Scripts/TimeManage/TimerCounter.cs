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

      public void FixedUpdate()
      {
         actualTime -= UnityEngine.Time.fixedDeltaTime;
      }

      public void Update()
      {
         actualTime -= UnityEngine.Time.deltaTime;
      }

      public bool CheckIfIntervalFinishFixedUpdate()
      {
         actualTime -= UnityEngine.Time.fixedDeltaTime;
         return actualTime <= 0;
      }

      public bool CheckIfIntervalFinishUpdate()
      {
         actualTime -= UnityEngine.Time.deltaTime;
         return actualTime <= 0;
      }

      public bool CheckIfIntervalAndResetFinishFixedUpdate()
      {
         actualTime -= UnityEngine.Time.fixedDeltaTime;

         if(actualTime <= 0)
         {
            Reset();
            return true;
         }

         return false;
      }

      public bool CheckIfIntervalAndResetFinishUpdate()
      {
         actualTime -= UnityEngine.Time.deltaTime;

         if(actualTime <= 0)
         {
            Reset();
            return true;
         }

         return false;
      }

      public float GetFactor()
      {
         return actualTime / time;
      }
   }
}