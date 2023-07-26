using UnityEngine;

namespace Extensions
{
   public class ExtensionRamdom : MonoBehaviour
   {
      public static T ChooseRamdomFromArray<T>(T[] argArray)
      {
         return argArray[Random.Range(0, argArray.Length)];
      }

      public static Vector3 RandomPointXZInSphere(float argScale = 1)
      {
         var tmpRandomAngle = Random.Range(0, Mathf.PI * 2);
         return new Vector3(Mathf.Cos(tmpRandomAngle), 0, Mathf.Sin(tmpRandomAngle)) * Random.Range(0f, argScale);
      }

      public static Vector3 RandomPointXZInSphereAroundPosition(Vector3 argPosition, float argScale = 1)
      {
         return RandomPointXZInSphere(argScale) + argPosition;
      }

      public static Vector3 RandomPointXZInCircunference(float argScale = 1)
      {
         var tmpRandomAngle = Random.Range(0, Mathf.PI * 2);
         return new Vector3(Mathf.Cos(tmpRandomAngle), 0, Mathf.Sin(tmpRandomAngle)) * argScale;
      }

      public static Vector3 RandomPointXZInCircunferenceAroundPosition(Vector3 argPosition, float argScale = 1)
      {
         return RandomPointXZInCircunference(argScale) + argPosition;
      }
   }
}