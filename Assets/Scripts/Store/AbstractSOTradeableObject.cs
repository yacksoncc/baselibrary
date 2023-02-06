using System;
using UnityEditor;
using UnityEngine;

namespace Store
{
   /// <summary>
   /// Base class for represent a tradeable object
   /// </summary>
   public abstract class AbstractSOTradeableObject : ScriptableObject
   {
      [SerializeField]
      private string id;

      [SerializeField]
      private string objectName;

      [SerializeField]
      private Sprite sprite;

      [SerializeField]
      private int price;

      [SerializeField]
      private SOCoin soCoinForBuyThisTradeableObject;

      public string ObjectName
         => objectName;

      public Sprite Sprite
         => sprite;

      public int Price
         => price;

      public SOCoin SoCoinForBuyThisTradeableObject
         => soCoinForBuyThisTradeableObject;

      public string ID
         => id;

      private void OnEnable()
      {
         GenerateNewID();
      }

      [ContextMenu("GenerateNewID")]
      private void GenerateNewID()
      {
#if UNITY_EDITOR
         if(id == "")
         {
            var tmpNewID = Guid.NewGuid().ToString();
            var tmpArraySplit = tmpNewID.Split('-');
            id = tmpArraySplit[0];
            EditorUtility.SetDirty(this);
         }
#endif
      }
   }
}