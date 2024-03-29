﻿using UnityEngine;

namespace Store
{
   public class StoreBasic : Inventory
   {
      [SerializeField]
      private InventoryBasic refInventoryBasic;

      public void BoughtStoreObject(OrderTrading argOrderTrading)
      {
         var tmpSoLotFinded = listSoLots.Find(argLoteObjeto => argLoteObjeto.RefSoTradeableObject == argOrderTrading.refSoTradeableObject);

         if(tmpSoLotFinded)
         {
            var tmpSoTradeableObject = argOrderTrading.refSoTradeableObject;
            var tmpQuantity = argOrderTrading.quantity;

            if(tmpSoLotFinded.ThereIsQuantity(tmpQuantity) && tmpSoTradeableObject.SoCoinForBuyThisTradeableObject.ConsumeThisQuantityOnlyIfThereIsEnough(tmpSoTradeableObject.Price * tmpQuantity))
            {
               tmpSoLotFinded.RemoveQuantity(tmpQuantity);
               refInventoryBasic.AddTradingOrderToThisInventory(argOrderTrading);
            }
            else
               Debug.Log("Can't bought object");
         }
      }
   }
}