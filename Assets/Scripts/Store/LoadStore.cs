using UnityEngine;

namespace Store
{
   public class LoadStore : MonoBehaviour
   {
      [SerializeField]
      private AbstractStore[] arrayAlmacenLotesForLoad;

      [SerializeField]
      private AbstractStore refAbstractStore;

      public AbstractStore RefAbstractStore
      {
         set => refAbstractStore = value;
      }

      private void Start()
      {
         CargarFromPlayersPrefs();
      }

      public void CargarFromPlayersPrefs()
      {
         var tmpJSONLoad = PlayerPrefs.GetString(refAbstractStore.StoreID, "{\"listObjetosTradeables\":[]}");
         var tmpListTradeableObjectsJSON = JsonUtility.FromJson<ListTradeableObjectsJSON>(tmpJSONLoad);
         LoadTradeableObjectsFromList(tmpListTradeableObjectsJSON);
      }

      public void CargarFromJSON(string argJSON)
      {
         var tmpListObjetosTradeablesJson = JsonUtility.FromJson<ListTradeableObjectsJSON>(argJSON);
         LoadTradeableObjectsFromList(tmpListObjetosTradeablesJson);
      }

      private void LoadTradeableObjectsFromList(ListTradeableObjectsJSON argListTradeableObjectsJSON)
      {
         foreach(var tmpIdTradeableObject in argListTradeableObjectsJSON.ListTradeableObjects)
            foreach(var tmpStore in arrayAlmacenLotesForLoad)
               foreach(var tmpSoLot in tmpStore.GetAllLotsAvaiblesOnStore())
                  if(tmpIdTradeableObject == tmpSoLot.RefSoTradeableObject.ID)
                     refAbstractStore.AddNewTradeableObject(tmpSoLot.RefSoTradeableObject, 1);
      }
   }
}