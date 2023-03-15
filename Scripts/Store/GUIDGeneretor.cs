using System;

namespace Store
{
   public static class GUIDGeneretor
   {
      public static void GenerateNewID(ref string argStringGUID)
      {
         if(argStringGUID == string.Empty)
         {
            var tmpNewID = Guid.NewGuid().ToString();
            argStringGUID = tmpNewID.Split('-')[0];
         }
      }
   }
}