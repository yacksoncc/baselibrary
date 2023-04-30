using UnityEngine;

namespace Store
{
   public class LotUIBasic : LotUI
   {
      [SerializeField]
      private string tagStore;

      private Inventory refStore;

      private int cantidadObjetosTradear = 1;

      private void Awake()
      {
         refStore = GameObject.FindWithTag(tagStore).GetComponent<Inventory>();
      }

      public override void ButtonExecuteTrade()
      {
         var tmpOrdenTrading = new OrderTrading
                               {
                                  quantity = cantidadObjetosTradear,
                                  refSoTradeableObject = soLot.RefSoTradeableObject
                               };

         Debug.Log(RefInventoryOwner.TradeLotFromThisInventoryToOtherInventory(tmpOrdenTrading, refStore)? "Object Bought" : "Object can't be Bought");
      }
   }
}