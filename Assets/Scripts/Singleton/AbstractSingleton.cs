#pragma warning disable 0649
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
                if (instance == null)
                {
                    var tmpObjectsOfType = FindInstanceInScene();

                    if (tmpObjectsOfType != null)
                        instance = tmpObjectsOfType;
                    else
                        instance = (new GameObject(typeof(T).ToString())).AddComponent<T>();
                }

                return instance;
            }
        }
        
        public static bool SingletonExist => instance != null;
        
        private static T FindInstanceInScene()
        {
            var tmpObjectsInScene = new List<T>();

            foreach (var tmpObjectFinded in Resources.FindObjectsOfTypeAll<T>())
            {
                if (tmpObjectFinded.hideFlags == HideFlags.NotEditable || tmpObjectFinded.hideFlags == HideFlags.HideAndDontSave)
                    continue;

#if UNITY_EDITOR
                if (EditorUtility.IsPersistent(tmpObjectFinded.gameObject))
                    continue;
#endif
                
                tmpObjectsInScene.Add(tmpObjectFinded.GetComponent<T>());
            }

            return tmpObjectsInScene.Count > 0 ? tmpObjectsInScene[0] : null;
        }
        
        public static void DestroySingleton()
        {
            Destroy(Instance.gameObject);
        }

        public static void CreateInstance()
        {
            DontDestroyOnLoad(Instance.gameObject);            
        }
    }
}