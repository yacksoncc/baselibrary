using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Extensions
{
   [Serializable]
   public class UnityEventVector3 : UnityEvent<Vector3>
   {
   }

   [Serializable]
   public class UnityEventBool : UnityEvent<bool>
   {
   }

   [Serializable]
   public class UnityEventBoolFloat : UnityEvent<bool, float>
   {
   }

   [Serializable]
   public class UnityEventFloat : UnityEvent<float>
   {
   }

   [Serializable]
   public class UnityEventString : UnityEvent<string>
   {
   }
   
   [Serializable]
   public class UnityEventTransform : UnityEvent<Transform>
   {
   }
   
   [Serializable]
   public class UnityEventGameObject : UnityEvent<GameObject>
   {
   }

   [Serializable]
   public class UnityEventPointerEventData : UnityEvent<PointerEventData>
   {
   }

   [Serializable]
   public class UnityEventInt : UnityEvent<int>
   {
   }
}