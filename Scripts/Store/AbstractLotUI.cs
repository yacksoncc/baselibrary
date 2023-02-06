using UnityEngine;
using UnityEngine.UI;

namespace Store
{
   /// <summary>
   /// Clase base basica para representar un lode de objeto en la UI, para mostrar los objetos de una tienda o inventario de usuario
   /// </summary>
   public abstract class AbstractLotUI : MonoBehaviour
   {
      [SerializeField]
      private Image imageIcon;

      protected SOLot RefSoLot;

      protected AbstractStore RefAbstractStoreOwner;

      public void SetLot(SOLot argSoLot, AbstractStore argRefAbstractStore)
      {
         RefAbstractStoreOwner = argRefAbstractStore;
         RefSoLot = argSoLot;
         imageIcon.sprite = RefSoLot.SpriteObjetoTienda;
      }

      public abstract void ButtonExecuteTrade();
   }
}