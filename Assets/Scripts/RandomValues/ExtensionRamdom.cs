using UnityEngine;

namespace RandomValues
{
   public class ExtensionRamdom : MonoBehaviour
   {
      public static T ChooseRamdomFromArray<T>(T[] argArray)
      {
         return argArray[Random.Range(0, argArray.Length)];
      }
   }
}