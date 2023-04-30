using UnityEngine;

namespace Store
{
   public abstract class InventoryUI : MonoBehaviour
   {
      [SerializeField]
      private RectTransform rectTransformContainerLots;

      [SerializeField]
      private GameObject prefabTradeableObjectUI;

      [SerializeField]
      protected Inventory refInventory;

      public Inventory RefInventory
         => refInventory;

      public virtual void CreateAllLotsOnContainerLots()
      {
         DestroyLotsPreviuslyCreatedOnUI();
         var tmpAllLotsAvaiblesInInventory = refInventory.GetAllLotsAvaiblesInInventory();

         foreach(var tmpSoLot in tmpAllLotsAvaiblesInInventory)
         {
            var tmpNewPrefabTradeableObjectUI = Instantiate(prefabTradeableObjectUI, rectTransformContainerLots);
            var tmpLotUI = tmpNewPrefabTradeableObjectUI.GetComponent<LotUI>();
            tmpLotUI.SetLot(tmpSoLot, refInventory);
         }
      }

      private void DestroyLotsPreviuslyCreatedOnUI()
      {
         while(rectTransformContainerLots.childCount > 0)
            DestroyImmediate(rectTransformContainerLots.GetChild(0).gameObject);
      }
   }
}