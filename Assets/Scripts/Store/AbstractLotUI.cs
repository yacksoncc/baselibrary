using UnityEngine;
using UnityEngine.UI;

namespace Store
{
   public abstract class AbstractLotUI : MonoBehaviour
   {
      [SerializeField]
      private Image imageIcon;

      protected SOLot RefSoLot;

      protected AbstractStore refAbstractStoreOwner;

      public void SetLot(SOLot argSoLot, AbstractStore argRefAbstractStore)
      {
         refAbstractStoreOwner = argRefAbstractStore;
         RefSoLot = argSoLot;
         imageIcon.sprite = RefSoLot.SpriteObjetoTienda;
      }

      public abstract void ButtonExecuteTrade();
   }
}