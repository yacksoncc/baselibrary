using UnityEngine;

#pragma warning disable

namespace Store
{
   public class LotUIBasico : AbstractLotUI
   {
      [SerializeField]
      private string tagAlmacenLotesParaTradear;

      private AbstractStore refStore;

      protected int cantidadObjetosTradear = 1;

      private void Awake()
      {
         refStore = GameObject.FindWithTag(tagAlmacenLotesParaTradear).GetComponent<AbstractStore>();
      }

      public override void ButtonExecuteTrade()
      {
         var tmpOrdenTrading = new OrderTrading
                               {
                                  quantity = cantidadObjetosTradear,
                                  refSoTradeableObject = RefSoLot.RefSoTradeableObject
                               };

         if(RefAbstractStoreOwner.TradeLotFromThisStoreToOtherStore(tmpOrdenTrading, refStore))
            Debug.Log("ObjetoComprado");
         else
            Debug.Log("Objeto no se pudo comprar");
      }
   }
}