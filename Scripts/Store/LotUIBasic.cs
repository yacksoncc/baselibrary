using UnityEngine;

namespace Store
{
   public class LotUIBasic : AbstractLotUI
   {
      [SerializeField]
      private string tagStore;

      private AbstractStore refStore;

      private int cantidadObjetosTradear = 1;

      private void Awake()
      {
         refStore = GameObject.FindWithTag(tagStore).GetComponent<AbstractStore>();
      }

      public override void ButtonExecuteTrade()
      {
         var tmpOrdenTrading = new OrderTrading
                               {
                                  quantity = cantidadObjetosTradear,
                                  refSoTradeableObject = RefSoLot.RefSoTradeableObject
                               };

         Debug.Log(refAbstractStoreOwner.TradeLotFromThisStoreToOtherStore(tmpOrdenTrading, refStore)? "Object Bought" : "Object can't be Bought");
      }
   }
}