using UnityEngine;

namespace LevelCurve
{
   public class TestLevelCurve : LevelCurve
   {
      protected override void Awake()
      {
         base.Awake();
         AddExperience(100);
         GetLeftExperienceToReachNextLevel();
         GetTotalExperienceToReachCurrentLevel();

         Debug.Log("GetGlobalFactorExperienceToReachNextLevel() = " + $"{GetGlobalFactorExperienceToReachNextLevel():F8}");
         Debug.Log("GetExperienceGainedOfCurrentLevel = " + $"{GetExperienceGainedOfCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceGained = " + $"{GetGlobalFactorExperienceGained():F8}");
         Debug.Log("GetFactorExperienceGainedToCurrentLevel() = " + $"{GetFactorExperienceGainedToCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceToReachActualLevel() = " + $"{GetGlobalFactorExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetExperienceToReachNextLevelFromActualLevel() = " + $"{GetTotalExperienceToReachNextLevelFromCurrentLevel():F8}");
         Debug.Log("GetTotalExperienceToReachNextLevel() = " + $"{GetTotalExperienceToReachNextLevel():F8}");
         Debug.Log("GetTotalExperienceToReachActualLevel() = " + $"{GetTotalExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetLeftExperienceToReachNextLevel() = " + $"{GetLeftExperienceToReachNextLevel():F8}");
         Debug.Log("----------------------------");

         AddExperience(50);

         Debug.Log("GetGlobalFactorExperienceToReachNextLevel() = " + $"{GetGlobalFactorExperienceToReachNextLevel():F8}");
         Debug.Log("GetExperienceGainedOfCurrentLevel = " + $"{GetExperienceGainedOfCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceGained = " + $"{GetGlobalFactorExperienceGained():F8}");
         Debug.Log("GetFactorExperienceGainedToCurrentLevel() = " + $"{GetFactorExperienceGainedToCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceToReachActualLevel() = " + $"{GetGlobalFactorExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetExperienceToReachNextLevelFromActualLevel() = " + $"{GetTotalExperienceToReachNextLevelFromCurrentLevel():F8}");
         Debug.Log("GetTotalExperienceToReachNextLevel() = " + $"{GetTotalExperienceToReachNextLevel():F8}");
         Debug.Log("GetTotalExperienceToReachActualLevel() = " + $"{GetTotalExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetLeftExperienceToReachNextLevel() = " + $"{GetLeftExperienceToReachNextLevel():F8}");
         Debug.Log("----------------------------");

         AddExperience(25);

         Debug.Log("GetGlobalFactorExperienceToReachNextLevel() = " + $"{GetGlobalFactorExperienceToReachNextLevel():F8}");
         Debug.Log("GetExperienceGainedOfCurrentLevel = " + $"{GetExperienceGainedOfCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceGained = " + $"{GetGlobalFactorExperienceGained():F8}");
         Debug.Log("GetFactorExperienceGainedToCurrentLevel() = " + $"{GetFactorExperienceGainedToCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceToReachActualLevel() = " + $"{GetGlobalFactorExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetExperienceToReachNextLevelFromActualLevel() = " + $"{GetTotalExperienceToReachNextLevelFromCurrentLevel():F8}");
         Debug.Log("GetTotalExperienceToReachNextLevel() = " + $"{GetTotalExperienceToReachNextLevel():F8}");
         Debug.Log("GetTotalExperienceToReachActualLevel() = " + $"{GetTotalExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetLeftExperienceToReachNextLevel() = " + $"{GetLeftExperienceToReachNextLevel():F8}");
         Debug.Log("----------------------------");

         AddExperience(75);

         Debug.Log("GetGlobalFactorExperienceToReachNextLevel() = " + $"{GetGlobalFactorExperienceToReachNextLevel():F8}");
         Debug.Log("GetExperienceGainedOfCurrentLevel = " + $"{GetExperienceGainedOfCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceGained = " + $"{GetGlobalFactorExperienceGained():F8}");
         Debug.Log("GetFactorExperienceGainedToCurrentLevel() = " + $"{GetFactorExperienceGainedToCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceToReachActualLevel() = " + $"{GetGlobalFactorExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetExperienceToReachNextLevelFromActualLevel() = " + $"{GetTotalExperienceToReachNextLevelFromCurrentLevel():F8}");
         Debug.Log("GetTotalExperienceToReachNextLevel() = " + $"{GetTotalExperienceToReachNextLevel():F8}");
         Debug.Log("GetTotalExperienceToReachActualLevel() = " + $"{GetTotalExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetLeftExperienceToReachNextLevel() = " + $"{GetLeftExperienceToReachNextLevel():F8}");
         Debug.Log("----------------------------");

         AddExperience(200);

         Debug.Log("GetGlobalFactorExperienceToReachNextLevel() = " + $"{GetGlobalFactorExperienceToReachNextLevel():F8}");
         Debug.Log("GetExperienceGainedOfCurrentLevel = " + $"{GetExperienceGainedOfCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceGained = " + $"{GetGlobalFactorExperienceGained():F8}");
         Debug.Log("GetFactorExperienceGainedToCurrentLevel() = " + $"{GetFactorExperienceGainedToCurrentLevel():F8}");
         Debug.Log("GetGlobalFactorExperienceToReachActualLevel() = " + $"{GetGlobalFactorExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetExperienceToReachNextLevelFromActualLevel() = " + $"{GetTotalExperienceToReachNextLevelFromCurrentLevel():F8}");
         Debug.Log("GetTotalExperienceToReachNextLevel() = " + $"{GetTotalExperienceToReachNextLevel():F8}");
         Debug.Log("GetTotalExperienceToReachActualLevel() = " + $"{GetTotalExperienceToReachCurrentLevel():F8}");
         Debug.Log("GetLeftExperienceToReachNextLevel() = " + $"{GetLeftExperienceToReachNextLevel():F8}");
      }

      protected override void OnLevelUp(ushort argCurrentLevel)
      {
         Debug.Log("OnLevelUp : " + argCurrentLevel);
      }

      protected override void OnTotalCurrentXpGained(double argXpGained)
      {
         Debug.Log("OnGainXp : " + argXpGained);
      }

      public override void TryLevelUP()
      {
         throw new System.NotImplementedException();
      }

      public override bool CanLevelUp()
      {
         throw new System.NotImplementedException();
      }
   }
}