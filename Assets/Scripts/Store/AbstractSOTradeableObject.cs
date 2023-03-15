using UnityEditor;
using UnityEngine;

namespace Store
{
   public abstract class AbstractSOTradeableObject : ScriptableObject
   {
      [SerializeField, HideInInspector]
      private string tradeableObjectId;

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

      public string TradeableObjectId
         => tradeableObjectId;

      private void OnEnable()
      {
         GenerateNewID();
      }

      private void GenerateNewID()
      {
         GUIDGeneretor.GenerateNewID(ref tradeableObjectId);
#if UNITY_EDITOR
         EditorUtility.SetDirty(this);
#endif
      }
   }
}