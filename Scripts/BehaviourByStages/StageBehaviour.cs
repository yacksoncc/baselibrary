using System;
using UnityEngine;

namespace BehaviourByStages
{
   [RequireComponent(typeof(StagesBehavioursController<>))]
   public abstract class StageBehaviour<T> : MonoBehaviour where T : Enum
   {
      [SerializeField]
      protected T stage;

      [SerializeField]
      private float stageWeight;

      private float actualStageWeight;

      private StagesBehavioursController<T> refStagesBehavioursController;

      public StagesBehavioursController<T> RefStagesBehavioursController
      {
         get
         {
            if(!refStagesBehavioursController)
               refStagesBehavioursController = GetComponent<StagesBehavioursController<T>>();

            return refStagesBehavioursController;
         }
      }

      public T Stage
         => stage;

      public float ActualStageWeight
      {
         get => actualStageWeight;
         set => actualStageWeight = value;
      }

      public float StageWeight
         => stageWeight;

      public void SetupWeight()
      {
         actualStageWeight = stageWeight;
      }

      public bool HasStageAnd(params Enum[] argStagesForEvaluating)
      {
         return RefStagesBehavioursController.HasStageAnd(argStagesForEvaluating);
      }

      public bool HasStageOr(params Enum[] argStagesForEvaluating)
      {
         return RefStagesBehavioursController.HasStageOr(argStagesForEvaluating);
      }

      public void AddStage(Enum argNewStage)
      {
         RefStagesBehavioursController.AddStage(argNewStage);
      }

      public void AddStage(params Enum[] argNewStages)
      {
         RefStagesBehavioursController.AddStage(argNewStages);
      }

      public void RemoveStage(Enum argNewStage)
      {
         RefStagesBehavioursController.RemoveStage(argNewStage);
      }

      public void RemoveStage(params Enum[] argNewStages)
      {
         RefStagesBehavioursController.RemoveStage(argNewStages);
      }

      public void RemoveAllStages()
      {
         RefStagesBehavioursController.RemoveAllStages();
      }

      public void SetStage(Enum argNewStage)
      {
         RefStagesBehavioursController.SetStage(argNewStage);
      }

      /// <summary>
      /// Si tiene el stage lo quita, si no lo tiene lo pone
      /// </summary>
      /// <param name="argStage">Stage para cambiar el estado</param>
      public void ToggleStage(Enum argStage)
      {
         RefStagesBehavioursController.ToggleStage(argStage);
      }
   }
}