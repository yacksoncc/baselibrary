#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace Singleton
{
   public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
   {
      protected static T instance;

      public static T Instance
      {
         get
         {
            if(instance == null)
            {
               var tmpObjectsOfType = FindInstanceInScene();

               if(tmpObjectsOfType != null)
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
         => instance != null;

      private static T FindInstanceInScene()
      {
         foreach(var tmpObjectFinded in Resources.FindObjectsOfTypeAll<T>())
         {
            if(tmpObjectFinded.hideFlags is HideFlags.NotEditable or HideFlags.HideAndDontSave)
               continue;

#if UNITY_EDITOR
            if(EditorUtility.IsPersistent(tmpObjectFinded.gameObject))
               continue;
#endif

            return tmpObjectFinded.GetComponent<T>();
         }

         return null;
      }

      public static void DestroySingleton()
      {
         if(instance != null)
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