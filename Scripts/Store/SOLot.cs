using UnityEngine;

namespace Store
{
   /// <summary>
   ///    Los lotes son usados para contener cantidades de un objeto, por ejemplo una tienda puede tener lotes de objetos, de donde el usuario comprar
   /// </summary>
   [CreateAssetMenu(fileName = "SOLoteObjeto", menuName = "Tienda/Lote Objeto", order = 0)]
   public class SOLot : ScriptableObject
   {
      [Header("Referencia objeto")]
      [SerializeField]
      private AbstractSOTradeableObject refSoTradeableObject;

      [Header("Cantidad objeto"), Tooltip("When this lot trade with other lot, lose quantity objects traded?")]
      [SerializeField]
      private bool consumeQuantityWhenTrade;

      [SerializeField]
      private int actualQuantity;

      public bool ConsumeQuantityWhenTrade
         => consumeQuantityWhenTrade;

      public int ActualQuantity
         => actualQuantity;

      public bool LotHasQuantityAvaible
         => actualQuantity > 0;

      public AbstractSOTradeableObject RefSoTradeableObject
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

      public void SetNewSOTradeableObject(AbstractSOTradeableObject argSoTradeableObject)
      {
         refSoTradeableObject = argSoTradeableObject;
      }

      public int GetWholePriceOfThisLot()
      {
         return actualQuantity * refSoTradeableObject.Price;
      }
   }
}