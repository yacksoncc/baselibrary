using System;

namespace Store
{
   [Serializable]
   public class SOTradeableObjectWrapperToSave
   {
      public string id;

      public int quantity;

      public SOTradeableObjectWrapperToSave(string argId, int argQuantity)
      {
         id = argId;
         quantity = argQuantity;
      }
   }
}