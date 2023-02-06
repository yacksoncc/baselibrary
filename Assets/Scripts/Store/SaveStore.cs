using UnityEngine;

namespace Store
{
   public class SaveStore : MonoBehaviour
   {
      public void Save(AbstractStore argStoreAGuardar)
      {
         var tmpListTradeableObjectsJson = new ListTradeableObjectsJSON();

         foreach(var tmpSoLot in argStoreAGuardar.GetAllLotsAvaiblesOnStore())
            tmpListTradeableObjectsJson.AddTradeableObject(tmpSoLot.RefSoTradeableObject);

         var tmpJsonSave = JsonUtility.ToJson(tmpListTradeableObjectsJson);
         PlayerPrefs.SetString(argStoreAGuardar.StoreID, tmpJsonSave);
      }
   }
}