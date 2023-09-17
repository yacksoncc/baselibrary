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

               if(tmpObjectsOfType == null)
                  Debug.LogWarning($"Singleton with name: {typeof(T)} does exits in scene, please setup.");
               else
                  instance = tmpObjectsOfType;
            }

            return instance;
         }
      }

      public static bool SingletonExist
         => instance != null;

      private static T FindInstanceInScene()
      {
         foreach(var tmpObjectFinded in FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            return tmpObjectFinded.GetComponent<T>();

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
   }
}