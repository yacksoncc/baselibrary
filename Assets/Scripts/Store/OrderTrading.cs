﻿namespace Store
{
   /// <summary>
   /// Used for trade with other store
   /// </summary>
   public class OrderTrading
   {
      public AbstractSOTradeableObject refSoTradeableObject;

      public int quantity;

      public int GetPriceForBuy()
      {
         return quantity * refSoTradeableObject.Price;
      }
   }
}