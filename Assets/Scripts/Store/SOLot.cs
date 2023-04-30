using UnityEngine;

namespace Store
{
   /// <summary>
   /// Lot must be used to store quantity of soTradeableObject
   /// </summary>
   [CreateAssetMenu(fileName = "soLot", menuName = "Store/soLot", order = 0)]
   public class SOLot : ScriptableObject
   {
      [SerializeField]
      private SOTradeableObject refSoTradeableObject;

      [SerializeField]
      private bool consumeQuantityWhenTrade;

      [SerializeField]
      private int actualQuantity;

      public bool ConsumeQuantityWhenTrade
         => consumeQuantityWhenTrade;

      public int ActualQuantity
         => actualQuantity;

      public bool HaveQuantityAvaible
         => actualQuantity > 0;

      public SOTradeableObject RefSoTradeableObject
         => refSoTradeableObject;

      public int PriceTradeableObject
         => refSoTradeableObject.Price;

      public string TradeableObjectName
         => refSoTradeableObject.ObjectName;

      public Sprite SpriteObjetoTienda
         => refSoTradeableObject.Sprite;

      public bool ThereIsQuantity(int argQuantity)
      {
         if(consumeQuantityWhenTrade)
            return ActualQuantity - argQuantity > 0;

         return true;
      }

      public void RemoveQuantity(int argQuantity)
      {
         actualQuantity -= argQuantity;
      }

      public void AddQuantity(int argQuantity)
      {
         actualQuantity += argQuantity;
      }
      
      public void SetQuantity(int argQuantity)
      {
         actualQuantity = argQuantity;
      }

      public void SetNewSOTradeableObject(SOTradeableObject argSoTradeableObject)
      {
         refSoTradeableObject = argSoTradeableObject;
      }

      public int GetWholePriceOfThisLot()
      {
         return actualQuantity * refSoTradeableObject.Price;
      }
   }
}