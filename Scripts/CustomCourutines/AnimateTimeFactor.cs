using UnityEngine;
using UnityEngine.Events;

namespace CustomCourutines
{
   public class AnimateTimeFactor : CustomYieldInstruction
   {
      private readonly UnityAction<float> CallbackNotifyTime;

      private readonly float maxTime;

      private float actualTime;

      public override bool keepWaiting
      {
         get
         {
            actualTime += Time.deltaTime;
            CallbackNotifyTime(Mathf.Clamp01(actualTime / maxTime));
            return actualTime <= maxTime;
         }
      }

      public AnimateTimeFactor(float argMaxTime, UnityAction<float> argCallbackNotifyTime)
      {
         CallbackNotifyTime = argCallbackNotifyTime;
         maxTime = argMaxTime;
         actualTime = 0;
      }
   }
}