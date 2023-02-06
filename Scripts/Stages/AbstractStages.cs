using System;
using UnityEngine;

namespace Stages
{
   public abstract class AbstractStages : MonoBehaviour
   {
      protected Enum actualStage;

      public abstract Enum ActualStage { protected get; set; }

      protected bool HasStage(Enum argStageForEvaluating)
      {
         if(actualStage == null)
            return false;

         return actualStage.HasFlag(argStageForEvaluating);
      }

      public bool HasStageAnd(params Enum[] argStagesForEvaluating)
      {
         var tmpValue = true;

         foreach(var tmpStage in argStagesForEvaluating)
            tmpValue = tmpValue && HasStage(tmpStage);

         return tmpValue;
      }

      public bool HasStageOr(params Enum[] argStagesForEvaluating)
      {
         var tmpValue = false;

         foreach(var tmpStage in argStagesForEvaluating)
            tmpValue = tmpValue || HasStage(tmpStage);

         return tmpValue;
      }

      public void AddStage(Enum argNewStage)
      {
         if(actualStage != null)
            ActualStage = (Enum)Enum.ToObject(actualStage.GetType(), Convert.ToUInt64(actualStage) | Convert.ToUInt64(argNewStage));
         else
            ActualStage = argNewStage;
      }

      public void AddStage(params Enum[] argNewStages)
      {
         if(actualStage != null)
         {
            var tmpActualStages = actualStage;

            foreach(var tmpStage in argNewStages)
               tmpActualStages = (Enum)Enum.ToObject(tmpActualStages.GetType(), Convert.ToUInt64(tmpActualStages) | Convert.ToUInt64(tmpStage));

            ActualStage = tmpActualStages;
         }
         else
         {
            var tmpActualStages = argNewStages[0];

            for(int i = 1; i < argNewStages.Length; i++)
               tmpActualStages = (Enum)Enum.ToObject(tmpActualStages.GetType(), Convert.ToUInt64(tmpActualStages) | Convert.ToUInt64(argNewStages[i]));

            ActualStage = tmpActualStages;
         }
      }

      public void RemoveStage(Enum argNewStage)
      {
         if(HasStageAnd(argNewStage))
            ActualStage = (Enum)Enum.ToObject(actualStage.GetType(), Convert.ToUInt64(actualStage) ^ Convert.ToUInt64(argNewStage));
      }

      public void RemoveStage(params Enum[] argNewStages)
      {
         var tmpActualStages = actualStage;

         foreach(var tmpStage in argNewStages)
            if(tmpActualStages.HasFlag(tmpStage))
               tmpActualStages = (Enum)Enum.ToObject(tmpActualStages.GetType(), Convert.ToUInt64(tmpActualStages) ^ Convert.ToUInt64(tmpStage));

         ActualStage = tmpActualStages;
      }

      public void RemoveAllStages()
      {
         ActualStage = default(Enum);
      }

      public void SetStage(Enum argNewStage)
      {
         ActualStage = argNewStage;
      }

      /// <summary>
      /// Si tiene el stage lo quita, si no lo tiene lo pone
      /// </summary>
      /// <param name="argStage">Stage para cambiar el estado</param>
      public void ToggleStage(Enum argStage)
      {
         if(actualStage == null)
            ActualStage = argStage;
         else
            ActualStage = (Enum)Enum.ToObject(actualStage.GetType(), Convert.ToUInt64(actualStage) ^ Convert.ToUInt64(argStage));
      }
   }
}