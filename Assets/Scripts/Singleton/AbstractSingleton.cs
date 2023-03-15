#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections.Generic;
using UnityEngine;

namespace Singleton
{
   public abstract class AbstractSingleton<T> : MonoBehaviour where T : AbstractSingleton<T>
   {
      protected static T instance;

      public static T Instance
      {
         get
         {
            if(instance is null)
            {
               var tmpObjectsOfType = FindInstanceInScene();

               if(tmpObjectsOfType is not null)
                  instance = tmpObjectsOfType;
               else
                  CreateGameObjectSingleton();
            }

            return instance;
         }
      }

      private static void CreateGameObjectSingleton()
      {
         instance = (new GameObject(typeof(T).ToString())).AddComponent<T>();
      }

      public static bool SingletonExist
         => instance is not null;

      private static T FindInstanceInScene()
      {
         var tmpObjectsInScene = new List<T>();

         foreach(var tmpObjectFinded in Resources.FindObjectsOfTypeAll<T>())
         {
            if(tmpObjectFinded.hideFlags is HideFlags.NotEditable or HideFlags.HideAndDontSave)
               continue;

#if UNITY_EDITOR
            if(EditorUtility.IsPersistent(tmpObjectFinded.gameObject))
               continue;
#endif

            tmpObjectsInScene.Add(tmpObjectFinded.GetComponent<T>());
         }

         return tmpObjectsInScene.Count > 0? tmpObjectsInScene[0] : null;
      }

      public static void DestroySingleton()
      {
         if(instance is not null)
            Destroy(instance.gameObject);
      }

      public static void CreateInstanceAndDontDestroy()
      {
         DontDestroyOnLoad(Instance.gameObject);
      }

      public static void CreateInstance()
      {
         CreateGameObjectSingleton();
      }
   }
}