using System;
using UnityEngine;

namespace Store
{
   [Serializable]
   public class SaveLoadInventoryPlayerPrefs : SaveLoadInventory
   {
      public override void Save(Inventory argInventoryToSave)
      {
         base.Load(argInventoryToSave);
         var tmpJsonSave = GetJSONInventory();
         Debug.Log($"Inventory with name {refInventory.name} saved {tmpJsonSave}");
         PlayerPrefs.SetString(argInventoryToSave.ID, tmpJsonSave);
      }

      public override void Load(Inventory argInventoryToLoad)
      {
         base.Load(argInventoryToLoad);
         var tmpJSON = PlayerPrefs.GetString(argInventoryToLoad.ID, "{}");
         SetupInventoryFromJSON(tmpJSON);
      }

      private void SetupInventoryFromJSON(string argJSON)
      {
         Debug.Log($"Inventory with name {refInventory.name} load {argJSON}");

         var tmpLoadStore = JsonUtility.FromJson<SaveLoadInventoryPlayerPrefs>(argJSON);
         arraySOTradeableObjectWrapper = tmpLoadStore.arraySOTradeableObjectWrapper;

         foreach(var tmpSoTradeableObjectWrapperToSave in arraySOTradeableObjectWrapper)
            refInventory.SetQuantityOfTradeableObjectById(tmpSoTradeableObjectWrapperToSave.id, tmpSoTradeableObjectWrapperToSave.quantity);
      }
   }
}