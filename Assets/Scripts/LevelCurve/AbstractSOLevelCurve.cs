using UnityEngine;

namespace LevelCurve
{
   public abstract class AbstractSOLevelCurve : ScriptableObject
   {
      [Range(0, 1000)]
      public ushort maxLevel;

      [Range(0, 10000)]
      public double maxExperienceToReachMaxLevel;

      public AnimationCurve curveLevelUp;
   }
}