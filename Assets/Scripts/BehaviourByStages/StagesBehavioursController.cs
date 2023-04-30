using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Stages;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BehaviourByStages
{
   public abstract class StagesBehavioursController<S> : Stages.Stages where S : Enum
   {
      protected readonly Collection<StageBehaviour<S>> listAllStages = new Collection<StageBehaviour<S>>();

      protected readonly Dictionary<Enum, StageBehaviour<S>> dictionaryStagesMonoBehaviour = new Dictionary<Enum, StageBehaviour<S>>();

      public override Enum ActualStage
      {
         protected get => actualStage;
         set
         {
            actualStage = value;

            foreach(var tmpStageBehaviour in listAllStages)
            {
               var tmpStage = tmpStageBehaviour.Stage;
               dictionaryStagesMonoBehaviour[tmpStage].enabled = HasStage(tmpStage);
            }
         }
      }

      protected virtual void Awake()
      {
         var tmpAllStagesBehaviour = GetComponents<StageBehaviour<S>>();

         foreach(var tmpStageBehaviour in tmpAllStagesBehaviour)
         {
            AddStageBehaviour(tmpStageBehaviour);
            tmpStageBehaviour.SetupWeight();
         }
      }

      public void ChooseRamdomStageWeighted(bool argIgnoreActualStage)
      {
         var tmpRamdomWeight = Random.Range(0f, 1f);
         var tmpPossibleListStaged = new List<StageRangeForRamdom>();

         if(argIgnoreActualStage)
         {
            var tmpWeightRest = dictionaryStagesMonoBehaviour[actualStage].ActualStageWeight;
            var tmpWeightOthersStages = 0f;

            foreach(var tmpAbstractStageBehaviour in dictionaryStagesMonoBehaviour.Values)
            {
               if((Convert.ToUInt64(tmpAbstractStageBehaviour.Stage) & Convert.ToUInt64(actualStage)) == Convert.ToUInt64(actualStage))
                  continue;

               tmpWeightOthersStages += tmpAbstractStageBehaviour.ActualStageWeight;
            }

            var tmpPreviousMinRange = 0f;

            foreach(var tmpAbstractStageBehaviour in dictionaryStagesMonoBehaviour.Values)
            {
               if((Convert.ToUInt64(tmpAbstractStageBehaviour.Stage) & Convert.ToUInt64(actualStage)) == Convert.ToUInt64(actualStage))
                  continue;

               var tmpMaxRange = tmpAbstractStageBehaviour.ActualStageWeight + (tmpAbstractStageBehaviour.ActualStageWeight / tmpWeightOthersStages) * tmpWeightRest;
               tmpPossibleListStaged.Add(new StageRangeForRamdom(tmpPreviousMinRange, tmpPreviousMinRange + tmpMaxRange, tmpAbstractStageBehaviour.Stage));
               tmpPreviousMinRange += tmpMaxRange;
            }
         }
         else
         {
            var tmpPreviousMinRange = 0f;

            foreach(var tmpAbstractStageBehaviour in dictionaryStagesMonoBehaviour.Values)
            {
               var tmpMaxRange = tmpAbstractStageBehaviour.ActualStageWeight;
               tmpPossibleListStaged.Add(new StageRangeForRamdom(tmpPreviousMinRange, tmpPreviousMinRange + tmpMaxRange, tmpAbstractStageBehaviour.Stage));
               tmpPreviousMinRange += tmpMaxRange;
            }
         }

         foreach(var tmpStageRangeForRamdom in tmpPossibleListStaged)
            if(tmpStageRangeForRamdom.CheckIfIntoRange(tmpRamdomWeight))
            {
               SetStage(tmpStageRangeForRamdom.stage);
               break;
            }
      }

      public void AddWeightToStage(S argStage, float argWeightForAdd)
      {
         var tmpWeightOthersStages = 0f;

         foreach(var tmpAbstractStageBehaviour in dictionaryStagesMonoBehaviour.Values)
         {
            if((Convert.ToUInt64(tmpAbstractStageBehaviour.Stage) & Convert.ToUInt64(argStage)) == Convert.ToUInt64(argStage))
               tmpAbstractStageBehaviour.ActualStageWeight += argWeightForAdd;
            else
               tmpWeightOthersStages += tmpAbstractStageBehaviour.ActualStageWeight;
         }

         foreach(var tmpAbstractStageBehaviour in dictionaryStagesMonoBehaviour.Values)
         {
            if((Convert.ToUInt64(tmpAbstractStageBehaviour.Stage) & Convert.ToUInt64(argStage)) == Convert.ToUInt64(argStage))
               continue;

            tmpAbstractStageBehaviour.ActualStageWeight -= (tmpAbstractStageBehaviour.ActualStageWeight / tmpWeightOthersStages) * argWeightForAdd;
         }
      }

      public void RemoveWeightToStage(S argStage, float argWeightForAdd)
      {
         var tmpWeightOthersStages = 0f;

         foreach(var tmpAbstractStageBehaviour in dictionaryStagesMonoBehaviour.Values)
         {
            if((Convert.ToUInt64(tmpAbstractStageBehaviour.Stage) & Convert.ToUInt64(argStage)) == Convert.ToUInt64(tmpAbstractStageBehaviour.Stage))
               tmpAbstractStageBehaviour.ActualStageWeight -= argWeightForAdd;
            else
               tmpWeightOthersStages += tmpAbstractStageBehaviour.ActualStageWeight;
         }

         foreach(var tmpAbstractStageBehaviour in dictionaryStagesMonoBehaviour.Values)
         {
            if((Convert.ToUInt64(tmpAbstractStageBehaviour.Stage) & Convert.ToUInt64(argStage)) == Convert.ToUInt64(tmpAbstractStageBehaviour.Stage))
               continue;

            tmpAbstractStageBehaviour.ActualStageWeight += (tmpAbstractStageBehaviour.ActualStageWeight / tmpWeightOthersStages) * argWeightForAdd;
         }
      }

      public void ResetAllWeights()
      {
         foreach(var tmpAbstractStageBehaviour in dictionaryStagesMonoBehaviour.Values)
            tmpAbstractStageBehaviour.SetupWeight();
      }

      public StageBehaviour<S> GetStageBehaviour(Enum argStage)
      {
         if(dictionaryStagesMonoBehaviour.TryGetValue(argStage, out var tmpStageBehaviour))
            return tmpStageBehaviour;

         Debug.LogError("Can't GetStageBehaviour because stage no exits");
         return null;
      }

      public void AddStageBehaviour(StageBehaviour<S> argStageBehaviour)
      {
         dictionaryStagesMonoBehaviour.Add(argStageBehaviour.Stage, argStageBehaviour);
         listAllStages.Add(argStageBehaviour);
      }

      private class StageRangeForRamdom
      {
         private readonly float minRange;

         private readonly float maxRange;

         public readonly S stage;

         public StageRangeForRamdom(float argMinRange, float argMaxRange, S argStage)
         {
            minRange = argMinRange;
            maxRange = argMaxRange;
            stage = argStage;
         }

         public bool CheckIfIntoRange(float argRamdomWeight)
         {
            return argRamdomWeight >= minRange && argRamdomWeight < maxRange;
         }
      }
   }
}