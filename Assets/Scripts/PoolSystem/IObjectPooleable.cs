using UnityEngine;

namespace PoolSystem
{
   /// <summary>
   /// Interface that must be implemented by any objects that needs be pooled
   /// </summary>
   public interface IObjectPooleable
   {
      /// <summary>
      /// For determine if and object is destroyed and can be reused, remember set this when the object was instanciate or destroyed
      /// </summary>
      bool Destroyed
      {
         get;
         set;
      }

      /// <summary>
      /// Notify when the object was created from exiting object in the pool
      /// </summary>
      /// <param name="argPosition">New position on world</param>
      /// <param name="argRotation">New Rotation on world</param>
      /// <param name="argParent">New parent</param>
      void InstantiateFromPool(Vector3 argPosition, Quaternion argRotation, Transform argParent = null);

      /// <summary>
      /// Notify when the object was created as new because not there is in the pool
      /// </summary>
      /// <param name="argPosition">New position on world</param>
      /// <param name="argRotation">New Rotation on world</param>
      /// <param name="argParent">New parent</param>
      /// <param name="argInitDefault"></param>
      void InstantiateFromPoolFirstTime(Vector3 argPosition, Quaternion argRotation, Transform argParent = null, bool argInitDefault = false);
        
      void DestroyPooleableObject();
      
      /// <summary>
      /// Get component from this object
      /// </summary>
      /// <typeparam name="T">Type of component that wants get</typeparam>
      T GetComponent<T>();
   }
}