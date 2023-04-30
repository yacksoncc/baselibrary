using System;
using UnityEngine;

namespace Store
{
   [Serializable]
   public abstract class SaveLoadInventory : ISaveInventory, ILoadInventory
   {
      public SOTradeableObjectWrapperToSave[] arraySOTradeableObjectWrapper;

      protected Inventory refInventory;

      public virtual void Save(Inventory argInventoryToSave)
      {
         refInventory = argInventoryToSave;
      }

      public virtual void Load(Inventory argInventoryToLoad)
      {
         refInventory = argInventoryToLoad;
      }

      protected string GetJSONInventory()
      {
         var tmpCollectionLotsAvaiblesInInventory = refInventory.GetAllLotsAvaiblesInInventory();
         arraySOTradeableObjectWrapper = new SOTradeableObjectWrapperToSave[tmpCollectionLotsAvaiblesInInventory.Count];

         for(int i = 0; i < arraySOTradeableObjectWrapper.Length; i++)
            arraySOTradeableObjectWrapper[i] = new SOTradeableObjectWrapperToSave(tmpCollectionLotsAvaiblesInInventory[i].RefSoTradeableObject.ID, tmpCollectionLotsAvaiblesInInventory[i].ActualQuantity);

         return JsonUtility.ToJson(this);
      }
   }
}