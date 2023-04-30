using System;
using UnityEngine;

namespace LevelCurve
{
   public abstract class LevelCurve : MonoBehaviour
   {
      [SerializeField]
      protected SOLevelCurve soLevelCurve;

      protected ushort currentLevel;

      protected double currentExperienceGained;

      protected double timeStepSizeToEachLevelInCurve;

      private ushort previousLevel;

      public bool MaxLevelReached
         => currentLevel == soLevelCurve.maxLevel;

      public ushort MaxLevel
         => soLevelCurve.maxLevel;

      private AnimationCurve CurveLevelUp
         => soLevelCurve.curveLevelUp;

      public double MaxExperienceToReachMaxLevel
         => soLevelCurve.maxExperienceToReachMaxLevel;

      public ushort CurrentLevel
         => currentLevel;

      public double CurrentExperienceGained
         => currentExperienceGained;

      protected virtual void Awake()
      {
         timeStepSizeToEachLevelInCurve = 1d / MaxLevel;
      }

      public void SetExperienceAtLevel(ushort argLevel)
      {
         currentLevel = argLevel;
         currentExperienceGained = GetTotalExperienceToReachCurrentLevel();
         NotifyCurrentLevel();
      }

      protected abstract void OnLevelUp(ushort argCurrentLevel);

      public double GetExperienceGainedOfCurrentLevel()
      {
         return currentExperienceGained - GetTotalExperienceToReachCurrentLevel();
      }

      public void AddExperience(double argExperienceAdd)
      {
         currentExperienceGained += argExperienceAdd;
         currentExperienceGained = Math.Clamp(currentExperienceGained, 0, MaxExperienceToReachMaxLevel);
         OnTotalCurrentXpGained(currentExperienceGained);
         UpdateCurrentLevelFromCurrentExperienceGained();
      }

      private void UpdateCurrentLevelFromCurrentExperienceGained()
      {
         ushort tmpLevel = currentLevel;

         for(double tmpValueOnCurve = currentLevel * timeStepSizeToEachLevelInCurve; tmpValueOnCurve <= 1d; tmpValueOnCurve += timeStepSizeToEachLevelInCurve)
         {
            var tmpCurveValue = CurveLevelUp.Evaluate(Convert.ToSingle(tmpValueOnCurve));

            if(tmpCurveValue * MaxExperienceToReachMaxLevel < currentExperienceGained)
               tmpLevel++;
            else
               break;
         }

         currentLevel = --tmpLevel;
         NotifyCurrentLevel();
      }

      private void NotifyCurrentLevel()
      {
         if(previousLevel == currentLevel)
            return;

         previousLevel = currentLevel;
         OnLevelUp(currentLevel);
      }

      protected abstract void OnTotalCurrentXpGained(double argXpGained);

      public double GetGlobalFactorExperienceToReachNextLevel()
      {
         double tmpNextLevel = currentLevel + 1d;
         return tmpNextLevel <= MaxLevel? CurveLevelUp.Evaluate(Convert.ToSingle(tmpNextLevel * timeStepSizeToEachLevelInCurve)) : 1d;
      }

      public double GetGlobalFactorExperienceToReachCurrentLevel()
      {
         return CurveLevelUp.Evaluate(Convert.ToSingle(currentLevel * timeStepSizeToEachLevelInCurve));
      }

      public double GetTotalExperienceToReachNextLevel()
      {
         return GetGlobalFactorExperienceToReachNextLevel() * MaxExperienceToReachMaxLevel;
      }

      public double GetTotalExperienceToReachCurrentLevel()
      {
         return CurveLevelUp.Evaluate(Convert.ToSingle(currentLevel * timeStepSizeToEachLevelInCurve)) * MaxExperienceToReachMaxLevel;
      }

      public double GetTotalExperienceToReachNextLevelFromCurrentLevel()
      {
         return GetTotalExperienceToReachNextLevel() - GetTotalExperienceToReachCurrentLevel();
      }

      public double GetLeftExperienceToReachNextLevel()
      {
         return GetTotalExperienceToReachNextLevelFromCurrentLevel() - GetExperienceGainedOfCurrentLevel();
      }

      public double GetGlobalFactorExperienceGained()
      {
         return currentExperienceGained / MaxExperienceToReachMaxLevel;
      }

      public double GetFactorExperienceGainedToCurrentLevel()
      {
         var tmpFactorDifferenceToNextLevel = GetGlobalFactorExperienceToReachNextLevel() - GetGlobalFactorExperienceToReachCurrentLevel();
         var tmpFactorDifferenceInCurrentLevel = GetGlobalFactorExperienceGained() - GetGlobalFactorExperienceToReachCurrentLevel();
         return tmpFactorDifferenceInCurrentLevel / tmpFactorDifferenceToNextLevel;
      }

      public abstract void TryLevelUP();

      public abstract bool CanLevelUp();
   }
}