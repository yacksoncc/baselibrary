using UnityEngine;

namespace Store
{
   public class LoadStore : MonoBehaviour
   {
      [SerializeField]
      private AbstractStore[] arrayStoresForLoad;

      [SerializeField]
      private AbstractStore refAbstractStore;

      public AbstractStore RefAbstractStore
      {
         set => refAbstractStore = value;
      }

      private void Start()
      {
         LoadFromPlayersPrefs();
      }

      public void LoadFromPlayersPrefs()
      {
         var tmpJSONLoad = PlayerPrefs.GetString(refAbstractStore.StoreID, "{\"listObjetosTradeables\":[]}");
         LoadFromJSON(tmpJSONLoad);
      }

      public void LoadFromJSON(string argJSON)
      {
         var tmpListObjetosTradeablesJson = JsonUtility.FromJson<ListTradeableObjectsJSON>(argJSON);
         LoadTradeableObjectsFromList(tmpListObjetosTradeablesJson);
      }

      private void LoadTradeableObjectsFromList(ListTradeableObjectsJSON argListTradeableObjectsJSON)
      {
         foreach(var tmpIdTradeableObject in argListTradeableObjectsJSON.ListTradeableObjects)
            foreach(var tmpStore in arrayStoresForLoad)
               foreach(var tmpSoLot in tmpStore.GetAllLotsAvaiblesOnStore())
                  if(tmpIdTradeableObject == tmpSoLot.RefSoTradeableObject.TradeableObjectId)
                     refAbstractStore.AddNewTradeableObject(tmpSoLot.RefSoTradeableObject, 1);
      }
   }
}