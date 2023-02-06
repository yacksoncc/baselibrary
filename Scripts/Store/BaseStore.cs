#pragma warning disable
using UnityEngine;

namespace Store
{
   public class BaseStore : AbstractStore
   {
      [SerializeField]
      private BaseInventory refBaseInventory;

      public void ComprarObjetoTienda(OrderTrading argOrderTrading)
      {
         var tmpSoLotFinded = listSoLots.Find(argLoteObjeto => argLoteObjeto.RefSoTradeableObject == argOrderTrading.refSoTradeableObject);

         if(tmpSoLotFinded)
         {
            var tmpSoTradeableObject = argOrderTrading.refSoTradeableObject;
            var tmpQuantity = argOrderTrading.quantity;

            if(tmpSoLotFinded.ThereIsQuantity(tmpQuantity) && tmpSoTradeableObject.SoCoinForBuyThisTradeableObject.ConsumeThisQuantityOnlyIfThereIsEnough(tmpSoTradeableObject.Price * tmpQuantity))
            {
               tmpSoLotFinded.RemoveQuantity(tmpQuantity);
               refBaseInventory.AddTradingOrder(argOrderTrading);
            }
            else
               Debug.Log("No se pudo tradear objeto");
         }
      }
   }
}