using System.Collections.Generic;
using UnityEditor;

namespace Utilities
{
   public class DefineSymbolsManipulation
   {
      public static bool RemoveOrAddDefine(string argDefineSymbol, bool argRemoveDefineSymbol)
      {
         var tmpCurrentDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
         var tmpHashSetDefines = new HashSet<string>();
         var tmpCurrentArr = tmpCurrentDefines.Split(';');

         //Add any define which doesn't contain MIRROR.
         foreach(string item in tmpCurrentArr)
            tmpHashSetDefines.Add(item);

         var tmpStartingCount = tmpHashSetDefines.Count;

         if(argRemoveDefineSymbol)
            tmpHashSetDefines.Remove(argDefineSymbol);
         else
            tmpHashSetDefines.Add(argDefineSymbol);

         bool modified = (tmpHashSetDefines.Count != tmpStartingCount);

         if(modified)
         {
            string changedDefines = string.Join(";", tmpHashSetDefines);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, changedDefines);
         }

         return modified;
      }
   }
}