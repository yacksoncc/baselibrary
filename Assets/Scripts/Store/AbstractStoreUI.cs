using System.Collections.Generic;
using UnityEngine;

namespace Store
{
   /// <summary>
   /// Base class to show Store in UI
   /// </summary>
   public abstract class AbstractStoreUI : MonoBehaviour
   {
      [SerializeField] private Transform transformContainerLots;

      [SerializeField] private GameObject prefabTradeableObjectUI;

      [SerializeField] protected AbstractStore refAbstractStore;

      private readonly List<AbstractLotUI> listTradeableObjectUI = new List<AbstractLotUI>();
      
      public virtual void CreateAllLotsOnContainerLots()
      {
         DestroyLotsPreviuslyCreatedOnUI();
         var tmpAllLotsAvaiblesOnStore = refAbstractStore.GetAllLotsAvaiblesOnStore();

         foreach(var tmpSoLot in tmpAllLotsAvaiblesOnStore)
         {
            var tmpNewPrefabTradeableObjectUI = Instantiate(prefabTradeableObjectUI, transformContainerLots);
            var tmpLotUI = tmpNewPrefabTradeableObjectUI.GetComponent<AbstractLotUI>();
            tmpLotUI.SetLot(tmpSoLot, refAbstractStore);
            listTradeableObjectUI.Add(tmpLotUI);
         }
      }

      private void DestroyLotsPreviuslyCreatedOnUI()
      {
         while(transformContainerLots.childCount > 0)
            DestroyImmediate(transformContainerLots.GetChild(0).gameObject);
         
         listTradeableObjectUI.Clear();
      }
   }
}